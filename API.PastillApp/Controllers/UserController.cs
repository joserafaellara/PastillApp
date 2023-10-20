using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using API.PastillApp.Services.Services;
using Azure.Core;
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
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await _userService.GetUser(userId);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener todos los usuarios: {ex.Message}");
            }
        }
        [HttpGet("{email}/user")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var result = await _userService.GetUserByEmail(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener un usuario por su email: {ex.Message}");
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDTO), 200)]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            var result = await _userService.CreateUser(createUserDTO);
            return result.isSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, UpdateUserDTO updateUserDTO)
        {
            updateUserDTO.UserId = userId;
            var result = await _userService.UpdateUser(updateUserDTO);
            return result.isSuccess ? Ok() : BadRequest(result);

        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(ResponseDTO), 200)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el usuario: {ex.Message}");
            }
        }

        [HttpPost("contact-emergency")]
        [ProducesResponseType(typeof(ResponseDTO), 200)]
        public async Task<IActionResult> EmergencyContactRequest(EmergencyContactRequestDTO request)
        {
            var result = await _userService.EmergencyContactRequest(request);
            return result.isSuccess ? Ok() : BadRequest(result);
        }

        [HttpGet("contact-emergency/request")]
        [ProducesResponseType(typeof(EmergencyRequestListDTO), 200)]
        public async Task<IActionResult> EmergencyContactResponse(string userMail)
        {
            var result = await _userService.EmergencyContactRecibedRequest(userMail);
            return Ok(result);
        }

        [HttpPost("contact-emergency/request")]
        [ProducesResponseType(typeof(ResponseDTO), 200)]
        public async Task<IActionResult> EmergencyContactResponse(EmergencyContactResponseDTO response)
        {
            var result = await _userService.EmergencyContactResponse(response);
            return Ok(result); 
        }
    }
}