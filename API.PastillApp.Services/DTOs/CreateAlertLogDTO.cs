using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class CreateAlertLogDTO
    {
        public int ReminderLogId { get; set; }
        public int? EmergencyUserId { get; set; }
        public DateTime LogDateTime { get; set; }
        public bool Taken { get; set; }
    }
}