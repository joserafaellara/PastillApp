using Microsoft.AspNetCore.Mvc;

using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API.PastillApp.Domain.Entities;

namespace API.PastillApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyStatusController : ControllerBase
    {
        private readonly IDailyStatusService _dailyStatusService;

        public DailyStatusController(IDailyStatusService dailyStatusService)
        {
            _dailyStatusService = dailyStatusService;
        }

        [HttpGet("{dailyStatusId}")]
        public async Task<IActionResult> GetDailyStatus(int dailyStatusId)
        {
            try
            {
                var result = await _dailyStatusService.GetDailyStatusById(dailyStatusId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el estado diario: {ex.Message}");
            }
        }

        [HttpGet("{userId}/dailyStatus")]
        public async Task<IActionResult> GetDailyStatusByUserId(int userId)
        {
            try
            {
                var result = await _dailyStatusService.GetDailyStatusByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los estados diarios por usuario: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDailyStatuses()
        {
            try
            {
                var result = await _dailyStatusService.GetAllDailyStatuses();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener todos los estados diarios: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDailyStatus(CreateDailyStatusDTO createDailyStatusDTO)
        {
            try
            {
                var result = await _dailyStatusService.CreateDailyStatus(createDailyStatusDTO);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el estado diario: {ex.Message}");
            }
        }

        [HttpPut("{dailyStatusId}")]
        public async Task<IActionResult> UpdateDailyStatus(int dailyStatusId, DailyStatus dailyStatus)
        {
            try
            {
                if (dailyStatusId != dailyStatus.DailyStatusID)
                    return BadRequest("ID de estado diario no coincide.");

                var result = await _dailyStatusService.UpdateDailyStatus(dailyStatus);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el estado diario: {ex.Message}");
            }
        }

        [HttpDelete("{dailyStatusId}")]
        public async Task<IActionResult> DeleteDailyStatus(int dailyStatusId)
        {
            try
            {
                var result = await _dailyStatusService.DeleteDailyStatus(dailyStatusId);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el estado diario: {ex.Message}");
            }
        }
    }
}
