using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class CreateTokenDTO
    {
        public string? DeviceToken { get; set; }
        public string? UserEmail { get; set; }
    }
}
