using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace API.PastillApp.Repositories
{
    internal class  ReminderRepository
    {
        private readonly PastillAppContext _context;

        public ReminderRepository(PastillAppContext context)
        {
            _context = context;
        }

        // CREATE (Add a new Reminder)
        public void AddReminder(Reminder reminder)
        {
            try
            {
                _context.Reminders.Add(reminder);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el recordatorio: {ex.Message}");
                throw;
            }
        }

        // READ (Get a reminder by ID)
        public Reminder GetReminderById(int reminderId)
        {
            try
            {
                return _context.Reminders.FirstOrDefault(r => r.ReminderId == reminderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el recordatorio por ID: {ex.Message}");
                throw;
            }
        }

        // READ (Get a reminder by Medicine ID.)
        public Reminder GetReminderByMedicineId(int medicineId)
        {
            try
            {
                return _context.Reminders.FirstOrDefault(r => r.MedicineId == medicineId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el recordatorio por Id de Medicina: {ex.Message}");
                throw;
            }
        }

        // READ (Get a reminder by User ID.)
        public Reminder GetReminderByUserId(int userId)
        {
            try
            {
                return _context.Reminders.FirstOrDefault(r => r.UserId == userId );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el recordatorio por Id de Usuario: {ex.Message}");
                throw;
            }
        }

        // READ (get all the reminders)
        public List<Reminder> GetAllReminder()
        {
            try
            {
                return _context.Reminders.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los recordatorios: {ex.Message}");
                throw;
            }
        }

        // UPDATE (Update a Reminder)
        public void UpdateReminder(Reminder reminder)
        {
            try
            {
                _context.Reminders.Update(reminder);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el recordatorio: {ex.Message}");
                throw;
            }
        }

        // DELETE (Delete a reminder by ID)
        public void DeleteReminder(int reminderId)
        {
            try
            {
                var reminder = _context.Reminders.FirstOrDefault(r => r.ReminderId == reminderId);
                if (reminder != null)
                {
                    _context.Reminders.Remove(reminder);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el recordatorio: {ex.Message}");
                throw;
            }
        }
    }
}