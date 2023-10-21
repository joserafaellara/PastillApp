using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class GetUserDTO
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? EmergencyUserId { get; set; }
        public string? EmergencyUser { get; set; }
    }
}
