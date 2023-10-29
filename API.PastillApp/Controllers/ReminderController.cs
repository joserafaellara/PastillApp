using Microsoft.AspNetCore.Mvc;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using API.PastillApp.Services.Services;
using API.PastillApp.Domain.Entities;

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


        [HttpGet]
        public async Task<IActionResult> GetAllReminders()
        {
            try
            {
                var result = await _reminderService.GetAllReminders();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener todos los recordatorios: {ex.Message}");
            }
        }

        [HttpGet("{userId}/reminder")]
        public async Task<IActionResult> GetReminderByUserId(int userId)
        {
            try
            {
                var result = await _reminderService.GetRemindersByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los recordatorios por usuario: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminder(CreateReminderDTO createReminderDTO)
        {
            var result = await _reminderService.CreateReminder(createReminderDTO);
            return result.isSuccess ? Ok(result) : BadRequest(result);
        }

       

        [HttpPut("{reminderId}")]
        public async Task<IActionResult> UpdateReminder(int reminderId, UpdateReminderDTO updateReminderDTO) 
        {
            try
            {
                if (reminderId != updateReminderDTO.ReminderId)
                    return BadRequest("ID de recordatorio no coincide.");

                var result = await _reminderService.UpdateReminder(updateReminderDTO);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el recordatorio: {ex.Message}");
            }
        }

        [HttpDelete("{reminderId}")]
        public async Task<IActionResult> DeleteReminder(int reminderId)
        {
            try
            {
                var result = await _reminderService.DeleteReminder(reminderId);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el recordatorio: {ex.Message}");
            }
        }

        //ES ESTE
        [HttpGet("reminderlogs/{userId}/{dateStart}/{dateFinish}")]
        public async Task<List<ReminderLogDTO>> GetLogsTimeLapseByUser(int userId, DateTime dateStart, DateTime dateFinish)
        {
            RemindersByUserIdDTO reminders = await _reminderService.GetRemindersByUserId(userId);
            List<ReminderLogDTO> reminderLogs = new List<ReminderLogDTO>();

            
            
               reminderLogs = await _reminderService.GetLogsByTimeLapse(reminders, dateStart, dateFinish);

            return reminderLogs;
        }




        [HttpGet("{reminderId}/reminderlogs")]
        public async Task<IActionResult> GetReminderLogsByReminderId(int reminderId)
        {
            try
            {
                var result = await _reminderService.GetReminderLogsByReminderId(reminderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los logs del recordatorio: {ex.Message}");
            }
        }

        [HttpGet("reminderLogsFromToday/{userId}")]
        public async Task<IActionResult> GetReminderLogsFromTodayByUserId(int userId)
        {
            var reminderLogDTOs = await _reminderService.GetReminderLogsFromTodayByUserId(userId);
            return Ok(reminderLogDTOs);
        }

        [HttpGet("active/{userId}")]
        public async Task<IActionResult> GetActiveRemindersByUserId(int userId)
        {
            try
            {
                var activeReminders = await _reminderService.GetActiveRemindersByUserId(userId);

                if (activeReminders.Count == 0)
                {
                    return NoContent();
                }

                return Ok(activeReminders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("reminderlogs/{reminderLogId}/taken")]
        public async Task<IActionResult> MarkReminderLogAsTaken(int reminderLogId)
        {
            try
            {
                var response = await _reminderService.ReminderLogTaken(reminderLogId);

                if (response.isSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO
                {
                    isSuccess = false,
                    message = $"Error al marcar el log como tomado: {ex.Message}"
                });
            }
        }

    }
}