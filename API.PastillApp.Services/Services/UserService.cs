using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;

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

        public Task<ResponseDTO> EmergencyContactRequest(EmergencyContactRequestDTO request)
        {
            var user = GetUserByEmail(request.UserMail).Result;

            var emergencyContactToken = _tokenService.GetTokenByUserEmail(request.ContactEmergencyMail).Result;
            if (emergencyContactToken == null)
                throw new Exception("Contacto de emergencia no encontrado");
            string body = user.Name + " " + user.LastName + " ha solicitado que seas su contacto de emergencia";

            return _tokenService.SendMessage("Solicitud de contacto de Emergencia", body, emergencyContactToken.DeviceToken!);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var user = await _userRepository.GetAllUsers();
            return user;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            email = email.ToLower();

            var user = await _userRepository.GetUserByEmail(email);

            return user;
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
    }
}
