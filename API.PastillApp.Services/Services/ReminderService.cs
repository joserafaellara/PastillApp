using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;

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
                var newReminder = _mapper.Map<Reminder>(reminder);

                // Add the new reminder to the data base (if you have an ReminderRepository)
                await _reminderRepository.AddReminder(newReminder);

                // Crear y devolver una respuesta de éxito
                var response = new ResponseDTO
                {
                    isSuccess = true,
                    message = "Recordatorio creado con éxito",
                };

                return response;
            }
            catch (Exception ex)
            {
                // En caso de error, crear y devolver una respuesta de error
                var errorResponse = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el recordatorio",
                };

                return errorResponse;
            };
        }

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
