using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.PastillApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await _userService.GetUser(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            var result = await _userService.CreateUser(createUserDTO);
            return result.isSuccess ? Ok() : BadRequest(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, UpdateUserDTO updateUserDTO)
        {
            updateUserDTO.UserId = userId;
            var result = await _userService.UpdateUser(updateUserDTO);
            return result.isSuccess ? Ok() : BadRequest(result);
        }
    }
}