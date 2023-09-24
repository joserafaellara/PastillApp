using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Domain.Entities
{
    public class AlertLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlertLogId { get; set; }

        [Required]
        [ForeignKey("ReminderLog")]
        public int ReminderLogId { get; set; }

        [ForeignKey("EmergencyUser")]
        public int? EmergencyUserId { get; set; }

        public virtual ReminderLog? ReminderLog { get; set; }
        public virtual User? EmergencyUser { get; set; }
    }
}
