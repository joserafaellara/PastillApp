using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;
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
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDTO> CreateUser(CreateUserDTO user)
        {
            try
            {
                var newUser = _mapper.Map<User>(user);

                // Agregar el nuevo usuario a la base de datos (asumiendo que tienes un UserRepository)
                await _userRepository.AddUser(newUser);

                // Crear y devolver una respuesta de éxito
                var response = new ResponseDTO
                {
                    isSuccess = true,
                    message = "Usuario creado con éxito",
                };

                return response;
            }
            catch (Exception ex)
            {
                // En caso de error, crear y devolver una respuesta de error
                var errorResponse = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el usuario",
                };

                return errorResponse;
            };
        }

        public Task<ResponseDTO> DeleteUser(int userId)
        {
            throw new NotImplementedException();
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

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
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
