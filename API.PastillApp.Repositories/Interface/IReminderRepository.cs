using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace API.PastillApp.Repositories.Interface
{
    public interface IReminderRepository
    {
        Task AddReminder(Reminder reminder);

        // READ 
        Task<Reminder> GetReminderById(int reminderId);

        // READ 
        Task<List<Reminder>> GetReminderByUserId(int userId);

        // READ 
        Task<List<Reminder>> GetAllReminders();

        // UPDATE 
        Task UpdateReminder(Reminder reminder);

        // DELETE
        Task DeleteReminder(Reminder reminder);
        Task BeginTransaction();
    }
}
