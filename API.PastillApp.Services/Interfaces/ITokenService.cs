using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;
using System.Threading.Tasks;

namespace API.PastillApp.Services.Interfaces
{
    public interface ITokenService
    {
        Task<ResponseDTO> CreateOrUpdateToken(CreateTokenDTO tokenDTO);
        Task<ResponseDTO> DeleteToken(string token);
        Task<Token> GetTokenByUserEmail(string userEmail);
        Task<IEnumerable<Token>> GetAllTokens();

    }
}