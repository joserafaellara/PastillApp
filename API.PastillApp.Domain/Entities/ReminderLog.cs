using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Domain.Entities
{
    public class ReminderLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReminderLogId { get; set; }

        [Required]
        [ForeignKey("Reminder")]
        public int ReminderId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public bool Taken { get; set; }

        public bool Notificated { get; set; }
        public bool SecondNotification { get; set; }
        public bool EmergencyNotification { get; set; }
        
        public virtual Reminder? Reminder { get; set; }

    }
}
