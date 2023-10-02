using Microsoft.AspNetCore.Mvc;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.Services;

namespace API.PastillApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet("{medicineId}")]
        [ProducesResponseType(typeof(Medicine), 200)]
        public async Task<IActionResult> GetMedicine(int medicineId)
        {
            var result = await _medicineService.GetMedicine(medicineId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicine(CreateMedicineDTO createMedicineDTO)
        {
            var result = await _medicineService.CreateMedicine(createMedicineDTO);
            return result.isSuccess ? Ok() : BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMedicine(int medicineId)
        {
            var result = await _medicineService.DeleteMedicine(medicineId);
            return result.isSuccess ? Ok() : BadRequest(result);
        }

        [HttpPut("{medicineId}")]
        public async Task<IActionResult> UpdateMedicine(int medicineId, Medicine medicine)
        {
            try
            {
                if (medicineId != medicine.MedicineId)
                    return BadRequest("ID de medicamento no coincide.");

                var result = await _medicineService.UpdateMedicine(medicine);
                return result.isSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el medicamento: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            try
            {
                var result = await _medicineService.GetAllMedicines();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener todos los medicamentos: {ex.Message}");
            }
        }

    }



}
