using Microsoft.AspNetCore.Mvc;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.PastillApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet("{reminderId}")]
        public async Task<IActionResult> GetReminder(int reminderId)
        {
            var result = await _reminderService.GetReminder(reminderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminder(CreateReminderDTO createReminderDTO)
        {
            var result = await _reminderService.CreateReminder(createReminderDTO);
            return result.isSuccess ? Ok() : BadRequest(result);
        }
    }
}
