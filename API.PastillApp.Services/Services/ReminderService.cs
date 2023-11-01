using API.PastillApp.Domain.Common;
using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Repositories.Migrations;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System;
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
        private readonly ITokenService _tokenService;
        public ReminderService(IReminderRepository reminderRepository, IReminderLogsRepository reminderLogsRepository, IMapper mapper, PastillAppContext pastillAppContext, ITokenService tokenService)
        {
            _reminderRepository = reminderRepository;
            _reminderLogsRepository = reminderLogsRepository;
            _context = pastillAppContext;
            _mapper = mapper;
            _tokenService = tokenService;
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
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var response = await _reminderRepository.GetReminderById(reminderId);
                if (response == null)
                {
                    return new ResponseDTO
                    {
                        isSuccess = false,
                        message = "Recordatorio no encontrado",
                    };
                }
                var reminderLogs = await _reminderLogsRepository.GetStartingFromDate(reminderId, DateTime.Now);
                await _reminderLogsRepository.DeleteGroup(reminderLogs);
                await _reminderRepository.DeleteReminder(reminderId);

                transaction.Commit();
                return new ResponseDTO() { isSuccess = true };
            }
            catch (Exception ex)
            {
                // Error inesperado
                transaction.Rollback();
                return new ResponseDTO() { isSuccess = false, message = ex.Message };
            }
        }

        public async Task<List<Reminder>> GetAllReminders()
        {
            var reminder = await _reminderRepository.GetAllReminders();
            return reminder;
        }


        public async Task<ReminderDTO> GetReminder(int reminderId)
        {
            var reminder = await _reminderRepository.GetReminderById(reminderId);
            var reminderDTO = _mapper.Map<ReminderDTO>(reminder);
            return reminderDTO;
        }

        public async Task<RemindersByUserIdDTO> GetRemindersByUserId(int userId)
        {
            var response = await _reminderRepository.GetReminderByUserId(userId);

            try
            {
                var remindersByUserId = new RemindersByUserIdDTO()
                {
                    RemindersByUserId = new List<ReminderDTO>()
                };

                foreach (var reminder in response)
                {
                    var reminderDTO = _mapper.Map<ReminderDTO>(reminder);
                    remindersByUserId.RemindersByUserId.Add(reminderDTO);
                }
                return remindersByUserId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ResponseDTO> UpdateReminder(UpdateReminderDTO reminder)
        {
            try
            {
                
                var reminderToUpdate = await _reminderRepository.GetReminderById(reminder.ReminderId);
                if (reminderToUpdate == null)
                {
                    return new ResponseDTO
                    {
                        isSuccess = false,
                        message = "Recordatorio no encontrado",
                    };
                }

                if (reminder.Quantity.HasValue)
                {
                    reminderToUpdate.Quantity = (double)reminder.Quantity;
                    
                }

                if (!string.IsNullOrWhiteSpace(reminder.Presentation))
                {
                    reminderToUpdate.Presentation = reminder.Presentation;
                }

                if (reminder.DateTimeStart.HasValue)
                {
                    reminderToUpdate.DateTimeStart = (DateTime)reminder.DateTimeStart;
                }
               
                if (reminder.FrequencyNumber.HasValue)
                {
                    reminderToUpdate.FrequencyNumber = (int)reminder.FrequencyNumber;
                }

                if (!string.IsNullOrWhiteSpace(reminder.FrequencyText))
                {
                    reminderToUpdate.FrequencyText = reminder.FrequencyText;
                }

                if (reminder.IntakeTimeNumber.HasValue)
                {
                    reminderToUpdate.IntakeTimeNumber = (int)reminder.IntakeTimeNumber;
                }

                if (!string.IsNullOrWhiteSpace(reminder.IntakeTimeText))
                {
                    reminderToUpdate.IntakeTimeText = reminder.IntakeTimeText;
                }

                if(reminderToUpdate.EmergencyAlert != reminder.EmergencyAlert)
                {
                    reminderToUpdate.EmergencyAlert = reminder.EmergencyAlert;
                }

                if(reminder.Observation != null)
                {
                    reminderToUpdate.Observation = reminder.Observation;
                }
                
                await _reminderRepository.UpdateReminder(reminderToUpdate);
               
                await UpdateLogs(reminderToUpdate, reminder.KeepPendingLogs);

                return new ResponseDTO
                {
                    isSuccess = true,
                    message = "Recordatorio actualizado",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el Recordatorio: {ex.Message}");
                return new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al actualizar el recordatorio",
                };
            }
        }

        public async Task UpdateLogs(Reminder reminder, Boolean keepPendingLogs)
        {
            var dateExpired = calculatedDateExpired(reminder.DateTimeStart, reminder.IntakeTimeText, reminder.IntakeTimeNumber);

            if (reminder.DateTimeStart >= dateExpired || reminder.FrequencyNumber <= 0)
            {
                throw new ArgumentException("Los parámetros no son válidos.");
            }

            TimeSpan frequency = calculateFrequency(reminder.FrequencyText, reminder.FrequencyNumber);

            reminder.EndDateTime = dateExpired;

            if (keepPendingLogs == true)
            {
                var lista = await _reminderLogsRepository.GetStartingFromDate(reminder.ReminderId, reminder.DateTimeStart);
                await _reminderLogsRepository.DeleteGroup(lista);
            }
            else
            {
                var lista = _reminderLogsRepository.GetStartingFromDate(reminder.ReminderId, DateTime.Now);
                await _reminderLogsRepository.DeleteGroup(lista.Result);
            }

            await createReminderLogs(reminder.DateTimeStart, frequency, dateExpired, reminder.ReminderId);
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

        public async Task SendAlarmNotification(ReminderLog reminderlog)
        {
            var mail = await _context.Users
             .Where(u => u.UserId == reminderlog.Reminder!.UserId)
             .Select(u => u.Email)
             .FirstOrDefaultAsync();

            await SendAlarm(mail);

            if(reminderlog.Notificated == true)
                reminderlog.SecondNotification = true;

            reminderlog.Notificated = true;

            await _reminderLogsRepository.UpdateReminderLog(reminderlog);
        }

        public async Task SendEmergencyNotification(ReminderLog reminderlog)
        {
            var user = await _context.Users
             .Where(u => u.UserId == reminderlog.Reminder!.UserId)
             .Include(u => u.EmergencyUser)
             .FirstOrDefaultAsync();

            if (user.EmergencyUser == null)
                await SendAlarm(user.Email);
            else    
                await SendEmergencyAlarm(user.EmergencyUser.Email, (user.Name + " " + user.LastName));

            reminderlog.EmergencyNotification = true;
            await _reminderLogsRepository.UpdateReminderLog(reminderlog);
        }

        private async Task SendAlarm(string mail)
        {
            try
            {
                var tokens = await _tokenService.GetTokensByUserEmail(mail);

                if (tokens.Count == 0)
                {
                    throw new Exception("No hay tokens para este usuario");
                }

                foreach (var token in tokens)
                {
                    var result = await _tokenService.SendMessage("ALARM", "Hora de tomar tu medicamento", token.DeviceToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar la alarma: {ex.Message}");
                throw;
            }
        }

        private async Task SendEmergencyAlarm(string mail, string userName)
        {
            try
            {
                var tokens = await _tokenService.GetTokensByUserEmail(mail);

                if (tokens.Count == 0)
                {
                    throw new Exception("No hay tokens para este usuario");
                }

                foreach (var token in tokens)
                {
                    var result = await _tokenService.SendMessage("EMERGENCY", $"{userName} no se ha tomado su medicamento", token.DeviceToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar la alarma de emergencia: {ex.Message}");
                throw;
            }
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
                    Taken = false,
                    Notificated = false,
                    SecondNotification = false,
                    EmergencyNotification = false
                    
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

        public async Task<List<ReminderLogDTO>> GetReminderLogsFromTodayByUserId(int userId)
        {
            var reminderLogs = await _reminderLogsRepository.GetReminderLogsFromTodayByUserId(userId);

            // Mapea los resultados a ReminderLogDTO
            var reminderLogDTOs = _mapper.Map<List<ReminderLogDTO>>(reminderLogs);

            return reminderLogDTOs;
        }

        public async Task<List<ReminderDTO>> GetActiveRemindersByUserId(int userId)
        {
            // Obtener la fecha actual
            DateTime today = DateTime.Now.Date;

            // Obtener los recordatorios activos cuya fecha de finalización es menor o igual a hoy
            List<Reminder> activeReminders = await _reminderRepository.GetActiveRemindersByUserId(userId, today);

            // Mapear los recordatorios activos a ReminderDTO
            List<ReminderDTO> activeReminderDTOs = _mapper.Map<List<ReminderDTO>>(activeReminders);

            return activeReminderDTOs;
        }

        public async Task<ResponseDTO> ReminderLogTaken(int reminderLogId)
        {
            try
            {
                var reminderLog = await _reminderLogsRepository.GetReminderLogById(reminderLogId);

                if (reminderLog != null)
                {
                    if (!reminderLog.Taken)
                    {
                        reminderLog.Taken = true;

                        await _reminderLogsRepository.UpdateReminderLog(reminderLog);

                        return new ResponseDTO
                        {
                            isSuccess = true,
                            message = "El log se ha marcado como tomado con éxito."
                        };
                    }
                    else
                    {
                        return new ResponseDTO
                        {
                            isSuccess = false,
                            message = "El log ya ha sido marcado como tomado anteriormente."
                        };
                    }
                }
                else
                {
                    return new ResponseDTO
                    {
                        isSuccess = false,
                        message = "Registro de ReminderLog no encontrado."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    isSuccess = false,
                    message = $"Error al marcar el log como tomado: {ex.Message}"
                };
            }
        }

        public async Task<List<ReminderLogDTO>> GetReminderLogsByDate(int userId, string dateString)
        {
            try
            {
                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    var reminderLogs = await _reminderLogsRepository.GetReminderLogsByDate(userId, date);
                    var reminderLogDTOs = _mapper.Map<List<ReminderLogDTO>>(reminderLogs);
                    return reminderLogDTOs;
                }
                else
                {
                    return new List<ReminderLogDTO>();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: puedes registrar el error, lanzar una excepción personalizada o realizar otras acciones necesarias.
                Console.WriteLine($"Error al obtener los registros de recordatorios por fecha: {ex.Message}");
                throw;
            }
        }

    }
}
