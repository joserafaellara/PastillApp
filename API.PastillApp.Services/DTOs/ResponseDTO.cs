using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.DTOs
{
    public class ResponseDTO
    {
        public bool isSuccess {  get; set; }
        public string? message { get; set; }
    }
}
