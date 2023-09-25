using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;


namespace API.PastillApp.Services.Interfaces
{
    public interface IReminderService
    {
        Task<Reminder> GetReminder(int reminderId);
        Task<Reminder> GetReminderByUserId(int userId);
        Task<List<Reminder>> GetAllReminders();
        Task<ResponseDTO> CreateReminder(CreateReminderDTO reminder);
        Task<ResponseDTO> UpdateReminder(Reminder reminder);
        Task<ResponseDTO> DeleteReminder(int reminderId);

    }
}