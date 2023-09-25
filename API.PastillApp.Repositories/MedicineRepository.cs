using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories
{
    internal class MedicineRepository
    {
        private readonly PastillAppContext _context;

        public MedicineRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Add new medicine)
        public async Task AddMedicine(Medicine medicine)
        {
            try
            {
                _context.Medicines.Add(medicine);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el medicamento: {ex.Message}");
                throw;
            }
        }

        // READ (Get medicine by ID)
        public async Task<Medicine> GetMedicineById(int medicineId)
        {
            try
            {
                return await _context.Medicines.FirstOrDefaultAsync(m => m.MedicineId == medicineId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el medicamento por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get all medicines)
        public async Task<List<Medicine>> GetAllMedicines()
        {
            try
            {
                return await _context.Medicines.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los medicamentos: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update a medicine)
        public async Task UpdateMedicine(Medicine medicine)
        {
            try
            {
                _context.Medicines.Update(medicine);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el medicamento: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete a medicine)
        public async Task DeleteMedicine(int medicineId)
        {
            try
            {
                var medicine = _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId);
                if (medicine != null)
                {
                    _context.Medicines.Remove(medicine);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el medicamento: {ex.Message}");
                throw;
            }
        }
    }
}