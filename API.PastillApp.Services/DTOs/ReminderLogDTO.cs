using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class ReminderLogDTO
    {
        public int ReminderLogId { get; set; }
        public int ReminderId { get; set; }
        public DateTime DateTime { get; set; }
        public bool Taken { get; set; }
        public int MedicineId { get; set; }
        public string? Name { get; set; }
        public int Dosage { get; set; }
        public string? Presentation { get; set; }
        public string? Observation { get; set; }
    }
}
