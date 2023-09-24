using API.PastillApp.Domain.Entities;
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
        public void AddMedicine(Medicine medicine)
        {
            try
            {
                _context.Medicines.Add(medicine);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el medicamento: {ex.Message}");
                throw;
            }
        }

        // READ (Get medicine by ID)
        public Medicine GetMedicineById(int medicineId)
        {
            try
            {
                return _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el medicamento por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get all medicines)
        public List<Medicine> GetAllMedicines()
        {
            try
            {
                return _context.Medicines.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los medicamentos: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update a medicine)
        public void UpdateMedicine(Medicine medicine)
        {
            try
            {
                _context.Medicines.Update(medicine);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el medicamento: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete a medicine)
        public void DeleteMedicine(int medicineId)
        {
            try
            {
                var medicine = _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId);
                if (medicine != null)
                {
                    _context.Medicines.Remove(medicine);
                    _context.SaveChanges();
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