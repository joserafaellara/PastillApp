using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories
{
    internal class ReminderLogRepository
    {
        private readonly PastillAppContext _context;

        public ReminderLogRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Add a new Reminder Log)
        public void AddReminderLog(ReminderLog reminderLog)
        {
            try
            {
                _context.ReminderLogs.Add(reminderLog);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el log del recordatorio: {ex.Message}");
                throw;
            }
        }

        // READ (Get a Reminder Log by ID.)
        public ReminderLog GetReminderLogById(int reminderLogId)
        {
            try
            {
                return _context.ReminderLogs.FirstOrDefault(r => r.ReminderLogId == reminderLogId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el log del  recordatorio por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get a Reminder Log by a reminder Id)
        public ReminderLog GetReminderByReminderId(int reminderId)
        {
            try
            {
                return _context.ReminderLogs.FirstOrDefault(r => r.ReminderId == reminderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el log del recordatorio por Id de recordatorio: {ex.Message}");
                throw;
            }
        }

        // READ (Get all the Reminder Logs)
        public List<ReminderLog> GetAllReminderLogs()
        {
            try
            {
                return _context.ReminderLogs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los logs de recordatorios: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update a Reminder Log)
        public void UpdateReminderLog(ReminderLog reminderLog)
        {
            try
            {
                _context.ReminderLogs.Update(reminderLog);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el log de recordatorio: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete a Reminder Log)
        public void DeleteReminderLog(int reminderLogId)
        {
            try
            {
                var reminderLog = _context.ReminderLogs.FirstOrDefault(r => r.ReminderLogId == reminderLogId);
                if (reminderLog != null)
                {
                    _context.ReminderLogs.Remove(reminderLog);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el log del recordatorio: {ex.Message}");
                throw;
            }
        }
    }

}

