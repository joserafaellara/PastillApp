using System;
using System.Threading.Tasks;
using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.PastillApp.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly PastillAppContext _context;

        public TokenRepository(PastillAppContext context)
        {
            _context = context;
        }

        public async Task AddToken(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<Token> GetTokenById(int tokenId)
        {
            return await _context.Tokens.FindAsync(tokenId);
        }

        public async Task<Token> GetTokenByValue(string tokenValue)
        {
            return await _context.Tokens.FirstOrDefaultAsync(t => t.DeviceToken == tokenValue);
        }

        public async Task UpdateToken(Token token)
        {
            _context.Entry(token).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteToken(Token token)
        {
            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Token>> GetTokensByUserEmail(string userEmail)
        {
            return await _context.Tokens
                .Where(t => t.UserEmail == userEmail.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Token>> GetAllTokens()
        {
            return await _context.Tokens.ToListAsync();
        }


    }
}
