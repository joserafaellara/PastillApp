using API.PastillApp.Domain.Entities;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories.Interface
{
    public interface ITokenRepository
    {
        Task AddToken(Token token);
        Task<Token> GetTokenById(int tokenId);
        Task<Token> GetTokenByValue(string tokenValue);
        Task UpdateToken(Token token);
        Task DeleteToken(Token token);
        Task<List<Token>> GetTokensByUserEmail(string userEmail);
        Task<IEnumerable<Token>> GetAllTokens();
    }
}
