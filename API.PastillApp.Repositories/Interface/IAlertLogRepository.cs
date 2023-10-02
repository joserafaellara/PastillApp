using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories.Interface
{
    public interface IAlertLogRepository
    {
        Task AddAlertLog(AlertLog alertLog);

        // READ (Get an alertLog by its Id)
        Task<AlertLog> GetAlertLogById(int alertLogId);

        // READ (Get all registered alertLog)
        Task<List<AlertLog>> GetAllAlertLogs();

        // UPDATE (Update a registered alertLog)
        Task UpdateAlertLog(AlertLog alertLog);

        // DELETE (Delete a registered alertLog)
        Task DeleteAlertLog(int alertLogId);

        // READ (Get registered alertLog by emergency user ID)
        Task <AlertLog> GetAlertLogByUserId(int userId);

        // READ (Get an alertLog by an specific reminder log)
        Task <AlertLog> GetAlertLogByReminderLogId(int reminderLogId);

    }
}