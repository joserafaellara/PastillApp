using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.Interfaces
{
    public interface IAlertLogService
    {
        Task<AlertLog> GetAlertLog(int alertLogId);
        Task<List<AlertLog>> GetAllAlertLogs();
        Task<List<AlertLog>> GetAllAlertLogsByUserId(int userId);
        Task<AlertLog> GetAlertLogByReminderLogId(int reminderLogId);
        Task<ResponseDTO> CreateAlertLog(CreateAlertLogDTO alertLogDTO);
        Task<ResponseDTO> UpdateAlertLog(AlertLog alertLog);
        Task<ResponseDTO> DeleteAlertLog(int alertLogId);

    }
}