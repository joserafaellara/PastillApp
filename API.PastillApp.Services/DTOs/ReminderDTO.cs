using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class ReminderDTO
    {
        public int ReminderId { get; set; }

        public double Quantity { get; set; }

        public string? Presentation { get; set; }

        public DateTime DateTimeStart { get; set; }

        public int FrequencyNumber { get; set; }

        public string FrequencyText { get; set; }

        public bool EmergencyAlert { get; set; }

        public string? Observation { get; set; }

        public int IntakeTimeNumber { get; set; }

        public string IntakeTimeText { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string? MedicineName { get; set; }
    }
}
