using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Domain.Entities
{
    public class ReminderDateTime
    {
        public int Id { get; set; } // Primary key
        public DateTime DateTimeValue { get; set; }
        public int ReminderId { get; set; } // Foreign key to associate with the Reminder
        public Reminder Reminder { get; set; } // Navigation property to access the associated Reminder
    }
}

