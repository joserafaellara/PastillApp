using API.PastillApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories
{
    public class PastillAppContext :  DbContext
    {
        public PastillAppContext(DbContextOptions options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reminder>()
                .HasMany(r => r.ReminderDateTimes)
                .WithOne(rd => rd.Reminder)
                .HasForeignKey(rd => rd.ReminderId);


            //Relaciones muchos a muchos
            //Nombre de tablas
            //Comportamientos especificos para manejo de entidades
        }





        public DbSet<AlertLog> AlertLogs { get; set; }
        public DbSet<DailyStatus> DailyStatuses { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ReminderLog> ReminderLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReminderDateTime> RemindersDateTimes { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
