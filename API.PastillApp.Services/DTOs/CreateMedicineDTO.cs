using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class CreateMedicineDTO
    {

        public int Dosage { get; set; }
        public string? Presentation { get; set; }
        public string? Name { get; set; }

    }
}
