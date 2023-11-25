using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.PastillApp.Repositories
{
    public class AlertLogRepository : IAlertLogRepository
    {
        private readonly PastillAppContext _context;

        public AlertLogRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Add a new Alert Log)
        public async Task AddAlertLog(AlertLog alertLog)
        {
            try
            {
                _context.AlertLogs.Add(alertLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la alerta: {ex.Message}");
                throw;
            }
        }

        // READ (Get an alert log by ID)
        public async Task<AlertLog> GetAlertLogById(int alertLogId)
        {
            try
            {
                return await _context.AlertLogs.FirstOrDefaultAsync(r => r.AlertLogId == alertLogId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la alerta por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get an Alert log by Reminder Log Id)
        public async Task<AlertLog> GetAlertLogByReminderLogId(int reminderLogId)
        {
            try
            {
                return await _context.AlertLogs.FirstOrDefaultAsync(r => r.ReminderLogId == reminderLogId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la alerta  por Id de Reminder Log: {ex.Message}");
                throw;
            }
        }

        // READ (Get an Alert log by EmergencyUser ID)
        public async Task<AlertLog> GetAlertLogByUserId(int userId)
        {
            /*try
            {
                return await _context.AlertLogs.FirstOrDefaultAsync(r => r.EmergencyUserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la alerta por Id de usuario de emergencia: {ex.Message}");
                throw;
            }

            ||

            public async Task<List<AlertLog>> GetAllAlertLogsByUserId(int emergencyUserId)
            {
            try
            {
            return await _context.AlertLogs
            .Where(r => r.EmergencyUserId == emergencyUserId)
            .ToListAsync();
            }
            catch (Exception ex)
            {
            Console.WriteLine($"Error al obtener las alertas por Id de Usuario de Emergencia: {ex.Message}");
            throw;
            }
           }

            */

            throw new NotImplementedException();
        }

        // READ (Get all the Alert logs)
        public async Task<List<AlertLog>> GetAllAlertLogs()
        {
            try
            {
                return await _context.AlertLogs.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todas las alertas: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update an alert log)
        public async Task UpdateAlertLog(AlertLog alertLog)
        {
            try
            {
                _context.AlertLogs.Update(alertLog);
                 await  _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la alerta: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete an alert log by ID)
        public async Task DeleteAlertLog(int alertLogId)
        {
            try
            {
                var alertLog = _context.AlertLogs.FirstOrDefault(r => r.AlertLogId == alertLogId);
                if (alertLog != null)
                {
                    _context.AlertLogs.Remove(alertLog);
                   await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la alerta: {ex.Message}");
                throw;
            }
        }

    }
}
