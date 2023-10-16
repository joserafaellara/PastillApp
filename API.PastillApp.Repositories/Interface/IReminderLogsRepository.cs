using API.PastillApp.Domain.Entities;
namespace API.PastillApp.Repositories.Interface
{
    public interface IReminderLogsRepository
    {
        Task AddReminderLogs(List<ReminderLog> reminderLogs);
        Task<List<ReminderLog>> GetbyReminderId(int reminderId);
        Task<List<ReminderLog>> GetReminderLogsNoTaken();
        Task<List<ReminderLog>> GetReminderLogsNoTakenNotificated();
        Task<List<ReminderLog>> GetReminderLogsToEmergency();
        Task UpdateReminderLog(ReminderLog reminderLog);
    }
}
