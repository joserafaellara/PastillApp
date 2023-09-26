using Microsoft.AspNetCore.Mvc;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;

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
    }
}
