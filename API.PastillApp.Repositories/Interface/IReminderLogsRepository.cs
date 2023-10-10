using API.PastillApp.Domain.Entities;
namespace API.PastillApp.Repositories.Interface
{
    public interface IReminderLogsRepository
    {
        Task AddReminderLogs(List<ReminderLog> reminderLogs);
        Task<List<ReminderLog>> GetbyReminderId(int reminderId);
    }
}
