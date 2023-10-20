using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PastillAppContext _context;

        public UserRepository(PastillAppContext context) 
        {
            _context = context;
        }

        // CREATE (Agregar un nuevo usuario)
        public async Task AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar un usuario: {ex.Message}");
                throw;
            }
        }

        // READ (Obtener un usuario por ID)
        public async Task<User> GetUserById(int userId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener un usuario por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Obtener un usuario por email)
        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener un usuario por Email: {ex.Message}");
                throw;
            }
        }

        // READ (Obtener todos los usuarios)
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los usuarios: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Actualizar un usuario)
        public async Task UpdateUser(User user)
        {
            try
            {
                 _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar un usuario: {ex.Message}");
                throw;
            }
        }

        // DELETE (Eliminar un usuario)
        public async Task DeleteUser(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar un usuario: {ex.Message}");
                throw;
            }
        }

        public async Task<List<EmergencyContactRequest>> GetRecibedRequest(string userMail)
        {
            var userId = _context.Users.Where(u => u.Email == userMail).Select(u => u.UserId).FirstOrDefault();

            return await _context.EmergencyContactRequests.Where(ecr => ecr.UserAnswerId == userId).Include(ucr => ucr.UserRequest).ToListAsync();
        }

        public async Task UpdateRequest(int requestId, bool accept)
        {
            var request = _context.EmergencyContactRequests.FirstOrDefault(ecr => ecr.EmergencyContactRequestId == requestId);
            request.Accept = accept;
            _context.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}