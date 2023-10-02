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

        public async Task<User> GetUserByEmail(string email)
        {
            email = email.ToLower();

            var user = await _userRepository.GetUserByEmail(email);

            return user;
        }

        public Task<ResponseDTO> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
