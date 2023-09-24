using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<Object> GetUser(int userId);
    }
}
