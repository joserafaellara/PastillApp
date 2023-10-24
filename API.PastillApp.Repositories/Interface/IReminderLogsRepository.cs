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
        Task<List<ReminderLog>> GetReminderLogsFromTodayByUserId(int userId);
        Task DeleteReminderLog(int reminderLogId);
        Task<List<ReminderLog>> GetStartingFromDate(int reminderId, DateTime date);
        Task DeleteGroup(List<ReminderLog> reminderLogs);

    }
}
