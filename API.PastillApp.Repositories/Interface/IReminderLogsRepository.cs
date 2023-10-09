using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories.Interface
{
    public interface IReminderLogsRepository
    {
        Task AddReminderLogs(List<ReminderLog> reminderLogs);
    }
}
