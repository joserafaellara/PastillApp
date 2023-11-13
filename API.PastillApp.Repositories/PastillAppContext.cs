using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.PastillApp.Repositories
{
    public class PastillAppContext :  DbContext
    {
        public PastillAppContext(DbContextOptions options) : base(options) {}
        public DbSet<AlertLog> AlertLogs { get; set; }
        public DbSet<DailyStatus> DailyStatuses { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ReminderLog> ReminderLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<EmergencyContactRequest> EmergencyContactRequests { get; set; }
    }
}
