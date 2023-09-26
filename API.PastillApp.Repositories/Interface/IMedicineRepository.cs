using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories.Interface
{
    public interface IMedicineRepository
    {
        Task AddMedicine(Medicine medicine);

        // READ
        Task<Medicine> GetMedicineById(int medicineId);

        // READ
        Task<Medicine> GetMedicineByName(string name);

        // READ 
        Task<List<Medicine>> GetAllMedicines();

        // UPDATE 
        Task UpdateMedicine(Medicine medicine);

        // DELETE 
        Task DeleteMedicine(int medicineId);
    }

}

