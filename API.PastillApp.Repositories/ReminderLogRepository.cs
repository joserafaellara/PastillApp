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
    public class ReminderLogRepository : IReminderLogsRepository
    {
        private readonly PastillAppContext _context;
       

        public ReminderLogRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Add a new Reminder Log)
        public async Task AddReminderLog(ReminderLog reminderLog)
        {
            try
            {
                _context.ReminderLogs.Add(reminderLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el log del recordatorio: {ex.Message}");
                throw;
            }
        }

        // READ (Get a Reminder Log by ID.)
        public async Task<ReminderLog> GetReminderLogById(int reminderLogId)
        {
            try
            {
                return await _context.ReminderLogs.FirstOrDefaultAsync(r => r.ReminderLogId == reminderLogId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el log del  recordatorio por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get a Reminder Log by a reminder Id)
        public async Task<ReminderLog> GetReminderByReminderId(int reminderId)
        {
            try
            {
                return await _context.ReminderLogs.FirstOrDefaultAsync(r => r.ReminderId == reminderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el log del recordatorio por Id de recordatorio: {ex.Message}");
                throw;
            }
        }

        // READ (Get all the Reminder Logs)
        public async Task<List<ReminderLog>> GetAllReminderLogs()
        {
            try
            {
                return await _context.ReminderLogs.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los logs de recordatorios: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update a Reminder Log)
        public async Task UpdateReminderLog(ReminderLog reminderLog)
        {
            try
            {
                _context.ReminderLogs.Update(reminderLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el log de recordatorio: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete a Reminder Log)
        public async Task DeleteReminderLog(int reminderLogId)
        {
            try
            {
                var reminderLog = _context.ReminderLogs.FirstOrDefault(r => r.ReminderLogId == reminderLogId);
                if (reminderLog != null)
                {
                    _context.ReminderLogs.Remove(reminderLog);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el log del recordatorio: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteGroup(List <ReminderLog> reminderLogs)
        {
            try
            {
                _context.ReminderLogs.RemoveRange(reminderLogs);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar logs del recordatorio: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ReminderLog>> GetStartingFromDate (int reminderId, DateTime date)
        {
            return await _context.ReminderLogs.Where(rl => rl.ReminderId == reminderId && rl.DateTime >= date).ToListAsync();
        }

       

        public async Task AddReminderLogs(List<ReminderLog> reminderLogs)
        {
            try
            {
                await _context.ReminderLogs.AddRangeAsync(reminderLogs);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar logs del recordatorio: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ReminderLog>> GetbyReminderId(int reminderId)
        {
            try
            {
                return await _context.ReminderLogs.Where(rl => rl.ReminderId == reminderId)
                                    .Include(rl => rl.Reminder)
                                        .ThenInclude(r => r.Medicine)
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar logs del recordatorio: {ex.Message}");
                throw;
            }
          
        }

        public async Task<List<ReminderLog>> GetReminderLogsNoTaken()
        {
            try
            {
                return await _context.ReminderLogs
                    .Where(rl => rl.Taken == false)
                    .Include(rl => rl.Reminder)
                    .OrderBy(rl=> rl.DateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los logs de recordatorios no tomados: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ReminderLog>> GetReminderLogsNoTakenNotificated()
        {
            try
            {
                return await _context.ReminderLogs
                    .Where(rl => rl.Taken == false & rl.Notificated == true)
                    .Include(rl => rl.Reminder)
                    .OrderBy(rl => rl.DateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los logs de recordatorios notificados y no tomados: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ReminderLog>> GetReminderLogsToEmergency()
        {
            try
            {
                return await _context.ReminderLogs
                    .Where(rl => rl.Taken == false & rl.SecondNotification == true)
                    .Include(rl => rl.Reminder)
                    .OrderBy(rl => rl.DateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los logs de recordatorios notificados y no tomados: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ReminderLog>> GetReminderLogsFromTodayByUserId(int userId)
        {
            try
            {
                DateTime today = DateTime.Today;
                return await _context.ReminderLogs
                    .Where(rl => rl.Reminder.UserId == userId && rl.DateTime.Date == today)
                    .Include(rl => rl.Reminder)
                    .Include(rl => rl.Reminder.Medicine)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los logs de recordatorios de hoy para el usuario {userId}: {ex.Message}");
                throw;
            }
        }
        public async Task<List<ReminderLog>> GetReminderLogsByDate(int userId, DateTime date)
        {
            return await _context.ReminderLogs
                .Where(rl => rl.Reminder.UserId == userId && rl.DateTime.Date == date)
                .Include(rl => rl.Reminder)
                .Include(rl => rl.Reminder.Medicine)
                .ToListAsync();
        }

    }

}

