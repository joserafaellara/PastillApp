using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class DailyStatusDTO
    {
        public int DailyStatusID { get; set; }
        public DateTime Date { get; set; }
        public string Symptoms { get; set; }
        public string Observation { get; set; }
    }
}
