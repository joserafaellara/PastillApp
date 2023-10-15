using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class UpdateReminderDTO
    {
        public int ReminderId { get; set; }
        public double Quantity { get; set; }
        public string? Presentation { get; set; }
        public DateTime DateTimeStart { get; set; }
        public int FrequencyNumber { get; set; }
        public string FrequencyText { get; set; }
        public int IntakeTimeNumber { get; set; }
        public string IntakeTimeText { get; set; }
    }
}
