using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class CreateReminderDTO
    {
        public int UserId { get; set; }

        public int MedicineId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateTimeStart { get; set; }

        public int FrequencyNumber { get; set; }

        public int FrequencyText { get; set; }

        public bool EmergencyAlert { get; set; }

        public string? Observation { get; set; }

        public int IntakeTimeNumber { get; set; }

        public string IntakeTimeText { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string Presentation { get; set; }

        public List<DateTime>? ReminderDateTimes { get; set; }

    }
}
