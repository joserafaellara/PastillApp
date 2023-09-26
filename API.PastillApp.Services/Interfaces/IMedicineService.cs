using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;


namespace API.PastillApp.Services.Interfaces
{
    public interface IMedicineService
    {
        Task<Medicine> GetMedicine(int medicineId);
        Task<Medicine> GetMedicineByName(string name);
        Task<List<Medicine>> GetAllMedicines();
        Task<ResponseDTO> CreateMedicine(CreateMedicineDTO medicine);
        Task<ResponseDTO> UpdateMedicine(Medicine medicine);
        Task<ResponseDTO> DeleteMedicine(int medicineId);

    }
}

