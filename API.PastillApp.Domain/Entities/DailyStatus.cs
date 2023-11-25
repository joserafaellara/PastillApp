using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Domain.Entities
{
    public class DailyStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DailyStatusID { get; set; }

        [Required(ErrorMessage = "El campo 'user_id' es obligatorio.")]
        [ForeignKey("User")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "El campo 'date' es obligatorio.")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [MaxLength(500, ErrorMessage = "La longitud máxima de 'symptoms' es de 500 caracteres.")]
        [Display(Name = "Síntomas")]
        public string? Symptoms { get; set; }

        [MaxLength(255, ErrorMessage = "La longitud máxima de 'observation' es de 255 caracteres.")]
        [Display(Name = "Observaciones")]
        public string? Observation { get; set; }

        public virtual User? User { get; set; }
    }
}
