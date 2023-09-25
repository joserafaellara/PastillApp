using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.PastillApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertLogController : ControllerBase
    {
        private readonly IAlertLogService _alertLogService;

        public AlertLogController(IAlertLogService alertLogService)
        {
            _alertLogService = alertLogService;
        }

        [HttpGet("{alertLogId}")]
        public async Task<IActionResult> GetAlertLog(int alertLogId)
        {
            var result = await _alertLogService.GetAlertLog(alertLogId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlertLog(CreateAlertLogDTO createAlertLogDTO)
        {
            var result = await _alertLogService.CreateAlertLog(createAlertLogDTO);
            if (result.isSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
