using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;


namespace API.PastillApp.Services.Interfaces
{
    public interface IReminderService
    {
        Task<ReminderDTO> GetReminder(int reminderId);
        Task<RemindersByUserIdDTO> GetRemindersByUserId(int userId);
        Task<List<Reminder>> GetAllReminders();
        Task<List<ReminderLogDTO>> GetLogsByTimeLapse(RemindersByUserIdDTO reminders, DateTime start, DateTime finish);
        Task<List<ReminderLog>> GetLogsByEachReminder(int reminderId, DateTime start, DateTime finish);
        Task<ResponseDTO> CreateReminder(CreateReminderDTO reminder);
        Task<ResponseDTO> UpdateReminder(UpdateReminderDTO reminder);
        Task<ResponseDTO> DeleteReminder(int reminderId);
        Task<ReminderLogsDTO> GetReminderLogsByReminderId(int reminderId);
        Task SendAlarmNotification(ReminderLog reminderlog);
        Task SendEmergencyNotification(ReminderLog reminderlog);
        Task<List<ReminderLogDTO>> GetReminderLogsFromTodayByUserId(int userId);
        Task<List<ReminderDTO>> GetActiveRemindersByUserId(int userId);
        Task<ResponseDTO> ReminderLogTaken(int userId);
        Task<List<ReminderLogDTO>> GetReminderLogsByDate(int userId, string dateString);
    }
}