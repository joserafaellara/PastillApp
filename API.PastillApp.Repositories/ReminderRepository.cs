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
    public class ReminderRepository : IReminderRepository
    {
        private readonly PastillAppContext _context;

        public ReminderRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Add a new Reminder)
        public async Task AddReminder(Reminder reminder)
        {
            try
            {
                _context.Reminders.Add(reminder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el recordatorio: {ex.Message}");
                throw;
            }
        }

        // READ (Get a reminder by ID)
        public async Task<Reminder> GetReminderById(int reminderId)
        {
            try
            {
                return await _context.Reminders.FirstOrDefaultAsync(r => r.ReminderId == reminderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el recordatorio por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get a reminder by Medicine ID.)
        public async Task<Reminder> GetReminderByMedicineId(int medicineId)
        {
            try
            {
                return await _context.Reminders.FirstOrDefaultAsync(r => r.MedicineId == medicineId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el recordatorio por Id de Medicina: {ex.Message}");
                throw;
            }
        }

        // READ (Get a reminder by User ID.)
        public async Task<List<Reminder>> GetReminderByUserId(int userId)
        {
            try
            {
                return await _context.Reminders
                    .Where(r => r.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los recordatorios por Id de Usuario: {ex.Message}");
                throw;
            }

        }

        // READ (get all the reminders)
        public async Task<List<Reminder>> GetAllReminders()
        {
            try
            {
                return await _context.Reminders.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los recordatorios: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update a Reminder)
        public async Task UpdateReminder(Reminder reminder)
        {
            try
            {
                _context.Reminders.Update(reminder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el recordatorio: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete a reminder by ID)
        public async Task DeleteReminder(int reminderId)
        {
            throw new NotImplementedException();
        }

        public Task BeginTransaction()
        {
            throw new NotImplementedException();
        }
    
    }
}