using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories
{
    internal class AlertLogRepository
    {
        private readonly PastillAppContext _context;

        public AlertLogRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Add a new Alert Log)
        public void AddAlertLog(AlertLog alertLog)
        {
            try
            {
                _context.AlertLogs.Add(alertLog);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la alerta: {ex.Message}");
                throw;
            }
        }

        // READ (Get an alert log by ID)
        public AlertLog GetAlertLogById(int alertLogId)
        {
            try
            {
                return _context.AlertLogs.FirstOrDefault(r => r.AlertLogId == alertLogId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la alerta por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get an Alert log by Reminder Log Id)
        public AlertLog GetAlertLogByReminderLogId(int reminderLogId)
        {
            try
            {
                return _context.AlertLogs.FirstOrDefault(r => r.ReminderLogId == reminderLogId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la alerta  por Id de Reminder Log: {ex.Message}");
                throw;
            }
        }

        // READ (Get an Alert log by EmergencyUser ID)
        public AlertLog GetAlertLogByEmergencyUserId(int EmergencyUserId)
        {
            try
            {
                return _context.AlertLogs.FirstOrDefault(r => r.EmergencyUserId == EmergencyUserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la alerta por Id de Usuario de Emergencia: {ex.Message}");
                throw;
            }
        }

        // READ (Get all the Alert logs)
        public List<AlertLog> GetAllAlertLogs()
        {
            try
            {
                return _context.AlertLogs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todas las alertas: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update an alert log)
        public void UpdateAlertLog(AlertLog alertLog)
        {
            try
            {
                _context.AlertLogs.Update(alertLog);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la alerta: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delet an alert log by ID)
        public void DeleteAlertLog(int alertLogId)
        {
            try
            {
                var alertLog = _context.AlertLogs.FirstOrDefault(r => r.AlertLogId == alertLogId);
                if (alertLog != null)
                {
                    _context.AlertLogs.Remove(alertLog);
                    _context.SaveChanges();
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
