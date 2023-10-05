using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;
using System.Diagnostics;

namespace API.PastillApp.Services.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IMapper _mapper;
        public ReminderService(IReminderRepository reminderRepository, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDTO> CreateReminder(CreateReminderDTO reminder)
        {   
            try
            {
                var response = new ResponseDTO
                {
                    isSuccess = true,
                    message = "Recordatorio creado con éxito",
                };
                var responseNeg = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el recordatorio",
                };

                string daily = "Diario";
                string weekly = "Semanal";
                string fortnightly = "Quincenal";
                string monthly = "Mensual";
                var newReminder = _mapper.Map<Reminder>(reminder);

                   // codigo a modificar:
               /* if ((newReminder.FrequencyNumber >= 1 && newReminder.FrequencyNumber <= 31) )
                {
                    if (newReminder.FrequencyText == daily || newReminder.FrequencyText == weekly || newReminder.FrequencyText == fortnightly || newReminder.FrequencyText == monthly)
                    {
                        DefineEndDate(newReminder);
                        DefineIntakeDateTimes(newReminder);
                        
                        await _reminderRepository.AddReminder(newReminder);
                    } else
                    {
                        response = responseNeg;
                    }
                } else
                {
                    response = responseNeg;
                }

               */
                
                return response;
            }
            catch (Exception ex)
            {
                
                var errorResponse = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el recordatorio",
                };

                return errorResponse;
            };
        }
         //Reescribir metodo
       /* public void DefineEndDate(Reminder reminder)
        {   if(reminder.IntakeDays == -2)
            {
                reminder.EndDateTime = reminder.DateTimeStart.AddDays(365);
            }else
            {
                reminder.EndDateTime = reminder.DateTimeStart.AddDays(reminder.IntakeDays);
            }
            
        }
       */

        //REESCRIBIR METODO
        /*private List<DateTime> DefineIntakeDateTimes(Reminder reminder)
        {
            List<DateTime> intakeDateTimes = new List<DateTime>();

            DateTime startTime = reminder.DateTimeStart;

            switch (reminder.FrequencyText)
            {
                case "Diaria":
                    while (startTime <= reminder.EndDateTime)
                    {
                        intakeDateTimes.Add(startTime);
                        startTime = startTime.AddDays(1);
                    }
                    break;

                case "Semanal":
                    while (startTime <= reminder.EndDateTime)
                    {
                        intakeDateTimes.Add(startTime);
                        startTime = startTime.AddDays(7 * reminder.FrequencyNumber);
                    }
                    break;

                case "Quincenal":
                    while (startTime <= reminder.EndDateTime)
                    {
                        intakeDateTimes.Add(startTime);
                        startTime = startTime.AddDays(14 * reminder.FrequencyNumber);
                    }
                    break;

                case "Mensual":
                    while (startTime <= reminder.EndDateTime)
                    {
                        intakeDateTimes.Add(startTime);
                        startTime = startTime.AddMonths(reminder.FrequencyNumber);
                    }
                    break;

                default:
                    // Handle an invalid frequency or other cases as needed
                    throw new ArgumentException("Frecuencia de toma no válida");
            }
            reminder.IntakeDateTimes = intakeDateTimes;
            return intakeDateTimes;
        }
        */





        public Task<ResponseDTO> DeleteReminder(int reminderId)
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

        public Task<Reminder> GetReminderByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> UpdateReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }
    }
}
