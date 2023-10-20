using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories.Interface
{
    public interface IUserRepository
    {
        Task AddUser(User user);

        // READ (Obtener un usuario por ID)
        Task<User> GetUserById(int userId);

        // READ (Obtener un usuario por email)
        Task<User> GetUserByEmail(string email);

        // READ (Obtener todos los usuarios)
        Task<List<User>> GetAllUsers();

        // UPDATE (Actualizar un usuario)
        Task UpdateUser(User user);

        // DELETE (Eliminar un usuario)
        Task DeleteUser(int userId);
        Task<List<EmergencyContactRequest>> GetRecibedRequest(string userMail);
        Task UpdateRequest(int requestId, bool accept);
    }
}
