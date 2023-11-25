using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.PastillApp.Services.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;
        public MedicineService(IMedicineRepository medicineRepository, IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDTO> CreateMedicine(CreateMedicineDTO medicine)
        {
            try
            {
                var newMedicine = _mapper.Map<Medicine>(medicine);

               
                await _medicineRepository.AddMedicine(newMedicine);

                
                var response = new ResponseDTO
                {
                    isSuccess = true,
                    message = "Medicamento creado con éxito",
                };

                return response;
            }
            catch (Exception ex)
            {
             
                var errorResponse = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el medicamento ",
                };

                return errorResponse;
            };
        }

        public async Task<ResponseDTO> DeleteMedicine(int medicineId)
        {
            try
            {
                await _medicineRepository.DeleteMedicine(medicineId);
                return new ResponseDTO { isSuccess = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el estado diario: {ex.Message}");
                return new ResponseDTO { isSuccess = false, message = "Error al eliminar el estado diario" };
            }
        }

        public async Task<List<Medicine>> GetAllMedicines()
        {
            var medicines = await _medicineRepository.GetAllMedicines();
            return medicines;
        }

        public async Task<Medicine> GetMedicine(int medicineId)
        {
            var medicine = await _medicineRepository.GetMedicineById(medicineId);
            return medicine;
        }

        public async Task<List<Medicine>> GetMedicineByName(string name)
        {
            string lowercaseName = name.ToLower();
            var medicines = await _medicineRepository.GetMedicineByName(lowercaseName);
            return medicines;
            
        }

        public async Task<ResponseDTO> UpdateMedicine(Medicine medicine)
        {
            try
            {
                // Update medicine in repository
                await _medicineRepository.UpdateMedicine(medicine);

                // Returns success if appropiate
                return new ResponseDTO { isSuccess = true, message = "Medicamento actualizado con éxito" };
            }
            catch (DbUpdateException dbEx)
            {
                // Specific exceptions in Databases errors
                Console.WriteLine($"Error de base de datos al actualizar el medicamento: {dbEx.Message}");
                return new ResponseDTO { isSuccess = false, message = "Error de base de datos al actualizar el medicamento" };
            }
            catch (Exception ex)
            {
                // Handles general exceptions
                Console.WriteLine($"Error al actualizar el medicamento: {ex.Message}");
                return new ResponseDTO { isSuccess = false, message = "Error al actualizar el medicamento" };
            }
        }

    }

}