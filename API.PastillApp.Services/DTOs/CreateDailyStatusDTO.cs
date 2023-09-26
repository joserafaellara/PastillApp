using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class CreateDailyStatusDTO
    {
        [Required(ErrorMessage = "El campo 'UserId' es obligatorio.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo 'Date' es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [MaxLength(500, ErrorMessage = "La longitud máxima de 'Symptoms' es de 500 caracteres.")]
        public string Symptoms { get; set; }

        [MaxLength(255, ErrorMessage = "La longitud máxima de 'Observation' es de 255 caracteres.")]
        public string Observation { get; set; }
    }
}