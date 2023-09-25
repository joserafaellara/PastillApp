﻿using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
    }

}
