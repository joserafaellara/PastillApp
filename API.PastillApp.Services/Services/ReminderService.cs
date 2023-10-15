using API.PastillApp.Domain.Common;
using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Repositories.Migrations;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;
using System.Runtime.Serialization.Formatters;
using System.Transactions;

namespace API.PastillApp.Services.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IReminderLogsRepository _reminderLogsRepository;
        private readonly PastillAppContext _context;
        private readonly IMapper _mapper;
        public ReminderService(IReminderRepository reminderRepository, IReminderLogsRepository reminderLogsRepository, IMapper mapper, PastillAppContext pastillAppContext)
        {
            _reminderRepository = reminderRepository;
            _reminderLogsRepository = reminderLogsRepository;
            _context = pastillAppContext;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateReminder(CreateReminderDTO createReminder)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var dateExpired = calculatedDateExpired(createReminder.DateTimeStart, createReminder.DurationType!, createReminder.DurationValue);

                if (createReminder.DateTimeStart >= dateExpired || createReminder.FrequencyValue <= 0)
                {
                    throw new ArgumentException("Los parámetros no son válidos.");
                }

                TimeSpan frequency = calculateFrequency(createReminder.FrequencyType, createReminder.FrequencyValue);

                var newReminder = _mapper.Map<Reminder>(createReminder);

                newReminder.EndDateTime = dateExpired;

                await _reminderRepository.AddReminder(newReminder);

                await createReminderLogs(createReminder.DateTimeStart, frequency, dateExpired, newReminder.ReminderId);

                transaction.Commit(); // Confirmar la transacción

                return new ResponseDTO() { isSuccess = true };
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Revertir la transacción en caso de error

                return new ResponseDTO() { isSuccess = false, message = ex.Message };
            }
        }
        public async Task<ResponseDTO> DeleteReminder(int reminderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reminder>> GetAllReminders()
        {
            var reminder = await _reminderRepository.GetAllReminders();
            return reminder;
        }

        public async Task<Reminder> GetReminder(int reminderId)
        {
            var reminder = await _reminderRepository.GetReminderById(reminderId);
            return reminder;
        }

        public async Task<Reminder> GetReminderByUserId(int userId)
        {
            try
            {
                var response = await _reminderRepository.GetReminderByUserId(userId);
                if (response == null){
                    throw new NullReferenceException();
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            };
        }

        public Task<ResponseDTO> UpdateReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }

        public async Task<ReminderLogsDTO> GetReminderLogsByReminderId(int reminderId)
        {
            try
            {
                var response = await _reminderLogsRepository.GetbyReminderId(reminderId);
                if (response == null)
                    return null;

                var reminderLogsDTO = new ReminderLogsDTO()
                {
                    LogsList = new List<ReminderLogDTO>()
                };
                               
                foreach (var log in response)
                {
                    var logDTO = new ReminderLogDTO()
                    {
                        ReminderLogId = log.ReminderLogId,
                        ReminderId = log.ReminderId, 
                        DateTime = log.DateTime,
                        Taken = log.Taken,
                        MedicineId = log.Reminder.MedicineId,
                        Name = log.Reminder.Medicine.Name,
                        Dosage = log.Reminder.Medicine.Dosage,
                        Presentation = log.Reminder.Presentation, 
                        Observation = log.Reminder.Observation
    };
                    reminderLogsDTO.LogsList.Add(logDTO);
                }
                return reminderLogsDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            };
        }

        private async Task createReminderLogs(DateTime dateTimeStart, TimeSpan frecuency, DateTime dateExpired, int reminderId)
        {
            List<ReminderLog> reminderLogs = new List<ReminderLog>();
            DateTime currentDateTime = dateTimeStart;

            while (currentDateTime < dateExpired)
            {
                reminderLogs.Add(new ReminderLog
                {
                    ReminderId = reminderId,
                    DateTime = currentDateTime,
                    Taken = false
                });

                currentDateTime = currentDateTime.Add(frecuency);
            }
           
            await _reminderLogsRepository.AddReminderLogs(reminderLogs);

        }

        private TimeSpan calculateFrequency(string? frequencyType, int frequencyValue)
        {
            TimeSpan frecuency;

            switch (frequencyType)
            {
                case Constants.Hour:
                    frecuency = TimeSpan.FromHours(frequencyValue);
                    break;
                case Constants.Day:
                    frecuency = TimeSpan.FromDays(frequencyValue);
                    break;
                case Constants.Week:
                    frecuency = TimeSpan.FromDays(7 * frequencyValue);
                    break;
                case Constants.Month:
                    frecuency = TimeSpan.FromDays(30 * frequencyValue); // mejorar
                    break;
                default:
                    throw new ArgumentException("Tipo de duración no válido.", nameof(frequencyType));
            }

            return frecuency;
        }

        private DateTime calculatedDateExpired(DateTime dateTimeStart, string durationType, int durationValue)
        {
            if (string.IsNullOrEmpty(durationType))
                throw new ArgumentException("El tipo de duración no puede estar vacío.", nameof(durationType));

            if (durationValue <= 0)
                throw new ArgumentException("El valor de duración debe ser mayor que cero.", nameof(durationValue));

            DateTime endDate;
            switch (durationType)
            {
                case Constants.Day:
                    endDate = dateTimeStart.AddDays(durationValue);
                    break;
                case Constants.Week:
                    endDate = dateTimeStart.AddDays(7*durationValue);
                    break;
                case Constants.Month:
                    endDate = dateTimeStart.AddMonths(durationValue);
                    break;
                case Constants.Unlimited:
                    endDate = dateTimeStart.AddDays(365);
                    break;
                default:
                    throw new ArgumentException("Tipo de duración no válido.", nameof(durationType));
            }

            return endDate;
        }


    }
}
