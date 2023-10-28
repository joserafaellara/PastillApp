using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace API.PastillApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<ResponseDTO> CreateUser(CreateUserDTO user)
        {
            try
            {
                var newUser = _mapper.Map<User>(user);

                await _userRepository.AddUser(newUser);

                var response = new ResponseDTO
                {
                    isSuccess = true,
                    message = "Usuario creado con éxito",
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el usuario : " + ex.Message,
                };

                return errorResponse;
            };
        }

        public Task<ResponseDTO> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<EmergencyRequestListDTO> EmergencyContactRecibedRequest(string userMail)
        {
            var result = await _userRepository.GetRecibedRequest(userMail);
            if (result.Count == 0)
                return null;

            var list = new EmergencyRequestListDTO { EmergencyRequestList = new List<EmergencyResquestDTO>() };
            foreach (var item in result)
            {
                list.EmergencyRequestList.Add(new EmergencyResquestDTO { 
                    EmergencyRequestId = item.EmergencyContactRequestId, Name = $"{item.UserRequest.Name} {item.UserRequest.LastName}" 
                });
            }
            return list;
        }

        public async Task<ResponseDTO> EmergencyContactRequest(EmergencyContactRequestDTO request)
        {
            var user = GetUserByEmail(request.UserMail).Result;
            var emergencyContact = GetUserByEmail(request.ContactEmergencyMail).Result;

            var emergencyContactTokens = await _tokenService.GetTokensByUserEmail(request.ContactEmergencyMail);

            if (emergencyContactTokens.Count == 0)
            {
                throw new Exception("Contacto de emergencia no encontrado");
            }

            string body = user.Name + " " + user.LastName + " ha solicitado que seas su contacto de emergencia";

            await _userRepository.CreateRequest(new EmergencyContactRequest { UserRequestId = user.UserId, UserAnswerId = emergencyContact.UserId });

            foreach (var token in emergencyContactTokens)
            {
                await _tokenService.SendMessage("Solicitud de contacto de Emergencia", body, token.DeviceToken);
            }

            return new ResponseDTO
            {
                isSuccess = true,
                message = "Solicitud de contacto de emergencia enviada con éxito",
            };
        }

        public async Task<ResponseDTO> EmergencyContactResponse(EmergencyContactResponseDTO request)
        {
            var response = new ResponseDTO();

            try
            {
                var emergencyRequest = await _userRepository.GetEmergencyRequestById(request.EmergencyRequestId);

                if (emergencyRequest != null)
                {
                    var user = await _userRepository.GetUserByEmail(emergencyRequest.UserRequest.Email);
                    var emergencyUser = await _userRepository.GetUserById(emergencyRequest.UserAnswerId);

                    // Obtener todos los tokens asociados a los usuarios
                    var userTokens = await _tokenService.GetTokensByUserEmail(user.Email);
                    var emergencyUserTokens = await _tokenService.GetTokensByUserEmail(emergencyUser.Email);

                    if (request.Accept)
                    {
                        user.EmergencyUserId = emergencyUser.UserId;
                        user.EmergencyUser = emergencyUser;

                        await _userRepository.UpdateUser(user);

                        // Enviar mensajes a todos los tokens asociados a los usuarios
                        foreach (var userToken in userTokens)
                        {
                            await _tokenService.SendMessage("Solicitud de contacto de emergencia", emergencyUser.Name + " ha aceptado tu solicitud!", userToken.DeviceToken);
                        }

                        foreach (var emergencyUserToken in emergencyUserTokens)
                        {
                            await _tokenService.SendMessage("Solicitud de contacto de emergencia", "Has aceptado la solicitud de contacto de emergencia de " + user.Name, emergencyUserToken.DeviceToken);
                        }

                        await _userRepository.UpdateRequest(request.EmergencyRequestId, request.Accept);
                    }
                    else
                    {
                        await _userRepository.DeleteEmergencyRequest(request.EmergencyRequestId);

                        // Enviar mensajes a todos los tokens asociados a los usuarios
                        foreach (var userToken in userTokens)
                        {
                            await _tokenService.SendMessage("Solicitud de contacto de emergencia", emergencyUser.Name + " ha rechazado tu solicitud.", userToken.DeviceToken);
                        }

                        foreach (var emergencyUserToken in emergencyUserTokens)
                        {
                            await _tokenService.SendMessage("Solicitud de contacto de emergencia", "Has rechazado la solicitud de contacto de emergencia de " + user.Name, emergencyUserToken.DeviceToken);
                        }
                    }

                    response.isSuccess = true;
                }
                else
                {
                    response.isSuccess = false;
                    response.message = "La solicitud de emergencia no se encontró";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDTO> SendEmergencyMessage(string userMail)
        {
            var response = new ResponseDTO();

            try
            {
                // Obtener el usuario con la sesión iniciada
                var user = await GetUserByEmail(userMail);

                if (user != null && user.EmergencyUser != null)
                {
                    var emergencyContactEmail = user.EmergencyUser;

                    // Obtener todos los tokens asociados al usuario y al contacto de emergencia
                    var userTokens = await _tokenService.GetTokensByUserEmail(userMail);
                    var emergencyContactTokens = await _tokenService.GetTokensByUserEmail(emergencyContactEmail);

                    if (userTokens.Count == 0)
                    {
                        response.isSuccess = false;
                        response.message = "No se encontraron tokens de notificación del usuario.";
                    }
                    else if (emergencyContactTokens.Count == 0)
                    {
                        response.isSuccess = false;
                        response.message = "No se encontraron tokens de notificación del contacto de emergencia.";
                    }
                    else
                    {
                        string body = user.Name + " " + user.LastName + " necesita ayuda con urgencia!!";

                        // Enviar el mensaje de emergencia a todos los tokens asociados al contacto de emergencia
                        foreach (var emergencyContactToken in emergencyContactTokens)
                        {
                            await _tokenService.SendMessage("EMERGENCIA", body, emergencyContactToken.DeviceToken);
                        }

                        response.isSuccess = true;
                        response.message = "Mensaje de emergencia enviado con éxito.";
                    }
                }
                else
                {
                    response.isSuccess = false;
                    response.message = "Usuario no encontrado o no tiene un contacto de emergencia configurado.";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }

            return response;
        }



        public async Task<List<GetUserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var userDtos = _mapper.Map<List<GetUserDTO>>(users);

            return userDtos;
        }

        public async Task<GetUserDTO> GetUser(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<GetUserDTO>(user);
        }

        public async Task<GetUserDTO> GetUserByEmail(string email)
        {
            email = email.ToLower();

            var user = await _userRepository.GetUserByEmail(email);
            return _mapper.Map<GetUserDTO>(user);
        }

        public async Task<ResponseDTO> UpdateUser(UpdateUserDTO user)
        {
            try
            {
                var userToUpdate = await _userRepository.GetUserById(user.UserId);
                if (userToUpdate == null)
                {
                    return new ResponseDTO
                    {
                        isSuccess = false,
                        message = "Usuario no encontrado",
                    };
                }

                if (!string.IsNullOrWhiteSpace(user.Name))
                {
                    userToUpdate.Name = user.Name;
                }

                if (!string.IsNullOrWhiteSpace(user.LastName))
                {
                    userToUpdate.LastName = user.LastName;
                }

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    userToUpdate.Email = user.Email;
                }

                await _userRepository.UpdateUser(userToUpdate);
                return new ResponseDTO
                {
                    isSuccess = true,
                    message = "Usuario actualizado",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el usuario: {ex.Message}");
                return new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al actualizar el usuario",
                };
            }
        }

        public async Task<ResponseDTO> DeleteEmergencyContact(string userMail)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(userMail);

                if (user == null)
                {
                    return new ResponseDTO
                    {
                        isSuccess = false,
                        message = "No se encontró ningún usuario con este correo electrónico."
                    };
                }

                if (user.EmergencyUserId == null)
                {
                    return new ResponseDTO
                    {
                        isSuccess = false,
                        message = "El usuario no tiene un contacto de emergencia."
                    };
                }

                user.EmergencyUserId = null;
                user.EmergencyUser = null;

                await _userRepository.UpdateUser(user);

                return new ResponseDTO
                {
                    isSuccess = true,
                    message = "Contacto de emergencia eliminado con éxito."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al eliminar el contacto de emergencia: " + ex.Message
                };
            }
        }



    }
}
