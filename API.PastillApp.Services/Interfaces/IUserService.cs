using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;


namespace API.PastillApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserDTO> GetUser(int userId);
        Task<GetUserDTO> GetUserByEmail(string email);
        Task<List<GetUserDTO>> GetAllUsers();
        Task<ResponseDTO> CreateUser(CreateUserDTO user);
        Task<ResponseDTO> UpdateUser(UpdateUserDTO user);
        Task<ResponseDTO> DeleteUser(int userId);
        Task<ResponseDTO> EmergencyContactRequest(EmergencyContactRequestDTO request);
        Task<EmergencyRequestListDTO> EmergencyContactRecibedRequest(string userMail);
        Task<ResponseDTO> EmergencyContactResponse(EmergencyContactResponseDTO request);
        Task<ResponseDTO> DeleteEmergencyContact(string userMail);
        Task<ResponseDTO> SendEmergencyMessage(string userMail);
    }
}
