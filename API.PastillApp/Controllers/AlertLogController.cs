using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using API.PastillApp.Services.Services;
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
            try
            {
                var result = await _alertLogService.GetAlertLog(alertLogId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener las alertas registradas: {ex.Message}"); ;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlertLog(CreateAlertLogDTO createAlertLogDTO)
        {
            var result = await _alertLogService.CreateAlertLog(createAlertLogDTO);
            return result.isSuccess ? Ok() : BadRequest(result);

        }

        [HttpGet("{userId}/alertLog")]
        public async Task<IActionResult> GetAllAlertLogsByUserId(int userId)
        {
            try
            {
                var result = await _alertLogService.GetAllAlertLogsByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener las alertas del usuario de emergencia: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlertLogs()
        {
            try
            {
                var result = await _alertLogService.GetAllAlertLogs();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener todas las alertas: {ex.Message}");
            }
        }

        [HttpPut("{alertLogId}")]
        public async Task<IActionResult> UpdateAlertLog(int alertLogId, AlertLog alertLog)
        {
            try
            {
                if (alertLogId != alertLog.AlertLogId)
                    return BadRequest("ID de alerta no coincide.");

                var result = await _alertLogService.UpdateAlertLog(alertLog);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la alerta: {ex.Message}");
            }
        }

        [HttpDelete("{alertLogId}")]
        public async Task<IActionResult> DeleteAlertLog(int alertLogId)
        {
            try
            {
                var result = await _alertLogService.DeleteAlertLog(alertLogId);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar la alerta: {ex.Message}");
            }
        }

        [HttpGet("rLId{reminderLogId}/alertLog")]
               public async Task<IActionResult> GetAlertLogByReminderLogId(int reminderLogId)
               {
                   try
                   {
                       var result = await _alertLogService.GetAlertLogByReminderLogId(reminderLogId);
                       return Ok(result);
                   }
                   catch (Exception ex)
                   {
                       return BadRequest($"Error al obtener la alerta del reminderLog especificado: {ex.Message}");
                   }
               }
           }
    }




