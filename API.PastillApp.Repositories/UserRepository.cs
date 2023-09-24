using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories
{
    internal class UserRepository
    {
        private readonly PastillAppContext _context;

        public UserRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Agregar un nuevo usuario)
        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar un usuario: {ex.Message}");
                throw;
            }
        }

        // READ (Obtener un usuario por ID)
        public User GetUserById(int userId)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener un usuario por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Obtener un usuario por email)
        public User GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener un usuario por Email: {ex.Message}");
                throw;
            }
        }

        // READ (Obtener todos los usuarios)
        public List<User> GetAllUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los usuarios: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Actualizar un usuario)
        public void UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar un usuario: {ex.Message}");
                throw;
            }
        }

        // DELETE (Eliminar un usuario)
        public void DeleteUser(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar un usuario: {ex.Message}");
                throw;
            }
        }
    }
}