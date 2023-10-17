using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;


namespace API.PastillApp.Services.Interfaces
{
    public interface IReminderService
    {
        Task<Reminder> GetReminder(int reminderId);
        Task<RemindersByUserIdDTO> GetReminderByUserId(int userId);
        Task<List<Reminder>> GetAllReminders();
        Task<ResponseDTO> CreateReminder(CreateReminderDTO reminder);
        Task<ResponseDTO> UpdateReminder(UpdateReminderDTO reminder);
        Task<ResponseDTO> DeleteReminder(int reminderId);
        Task<ReminderLogsDTO> GetReminderLogsByReminderId(int reminderId);
    }
}