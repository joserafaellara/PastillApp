using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;


namespace API.PastillApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(int userId);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<ResponseDTO> CreateUser(CreateUserDTO user);
        Task<ResponseDTO> UpdateUser(User user);
        Task<ResponseDTO> DeleteUser(int userId);

    }
}
