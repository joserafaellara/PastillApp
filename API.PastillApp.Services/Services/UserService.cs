using API.PastillApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.Services
{
    public class UserService : IUserService
    {
        public Task<object> GetUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
