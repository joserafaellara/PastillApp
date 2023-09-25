using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories
{
    internal class DailyStatusRepository
    {
        private readonly PastillAppContext _context;

        public DailyStatusRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Adding a new Daily Status)
        public async Task AddDailyStatus(DailyStatus dailyStatus)
        {
            try
            {
                _context.DailyStatuses.Add(dailyStatus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar un estado dario: {ex.Message}");
                throw;
            }
        }

        // READ (Obtaining a daily status by it's ID)
        public async Task <DailyStatus> GetDailyStatusById(int dailyStatusId)
        {
            try
            {
                return await _context.DailyStatuses.FirstOrDefaultAsync(u => u.DailyStatusID == dailyStatusId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener un estado diario por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Obtain all daily statuses)
        public async Task<List<DailyStatus>> GetAllDailyStatus()
        {
            try
            {
                return await _context.DailyStatuses.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los estados diarios: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update a daily status)
        public async Task UpdateDailyStatus(DailyStatus dailyStatus)
        {
            try
            {
                _context.DailyStatuses.Update(dailyStatus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar un estado diario: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete a daily status)
        public async Task DeleteDailyStatus(int dailyStatusId)
        {
            try
            {
                var dailyStatus = _context.DailyStatuses.FirstOrDefault(u => u.DailyStatusID == dailyStatusId);
                if (dailyStatus != null)
                {
                    _context.DailyStatuses.Remove(dailyStatus);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar un estado diario: {ex.Message}");
                throw;
            }
        }

    }
}
