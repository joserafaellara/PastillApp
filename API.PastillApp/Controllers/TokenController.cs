using System.Threading.Tasks;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.PastillApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // POST: api/Token
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateToken(CreateTokenDTO createTokenDTO)
        {
            var result = await _tokenService.CreateOrUpdateToken(createTokenDTO);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // DELETE: api/Token
        [HttpDelete]
        public async Task<IActionResult> DeleteToken(string tokenValue)
        {
            var result = await _tokenService.DeleteToken(tokenValue);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{userEmail}")]
        public async Task<IActionResult> GetTokenByUserEmail(string userEmail)
        {
            var result = await _tokenService.GetTokenByUserEmail(userEmail);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTokens()
        {
            var tokens = await _tokenService.GetAllTokens();

            return Ok(tokens);
        }
    }
}
