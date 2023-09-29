using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class UpdateDailyStatusDTO
    {
        public int DailyStatusID { get; set; }

        [MaxLength(500, ErrorMessage = "La longitud máxima de 'Symptoms' es de 500 caracteres.")]
        public string? Symptoms { get; set; }

        [MaxLength(255, ErrorMessage = "La longitud máxima de 'Observation' es de 255 caracteres.")]
        public string? Observation { get; set; }
    }
}
