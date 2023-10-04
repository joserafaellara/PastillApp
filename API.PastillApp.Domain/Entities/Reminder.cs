using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Domain.Entities
{
    public class Reminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReminderId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string? Presentation { get; set; }

        [Required]
        public DateTime DateTimeStart { get; set; }

        [Required]
        public int FrequencyNumber { get; set; }

        [Required]
        public string FrequencyText { get; set; }




        [Required]
        public bool EmergencyAlert { get; set; }

        [MaxLength(255, ErrorMessage = "La longitud máxima de 'observation' es de 255 caracteres.")]
        public string? Observation { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }


        public virtual User? User { get; set; }
        public virtual Medicine? Medicine{ get; set; }

    }
}
