using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.Services
{
    public class AlertLogService : IAlertLogService
    {
        private readonly IAlertLogRepository _alertLogRepository;
        private readonly IMapper _mapper;

        public AlertLogService(IAlertLogRepository alertLogRepository, IMapper mapper)
        {
            _alertLogRepository = alertLogRepository;
            _mapper = mapper;
        }

        public async Task<AlertLog> GetAlertLog(int alertLogId)
        {
            return await _alertLogRepository.GetAlertLogById(alertLogId);
        }

        public async Task<List<AlertLog>> GetAllAlertLogs()
        {
            return await _alertLogRepository.GetAllAlertLogs();
        }

        public async Task<ResponseDTO> CreateAlertLog(CreateAlertLogDTO alertLogDTO)
        {
            try
            {
                var alertLog = _mapper.Map<AlertLog>(alertLogDTO);

                // Agregar el nuevo registro de AlertLog a la base de datos
                await _alertLogRepository.AddAlertLog(alertLog);

                // Crear y devolver una respuesta de éxito
                var response = new ResponseDTO
                {
                    isSuccess = true,
                    message = "Registro de AlertLog creado con éxito",
                };

                return response;
            }
            catch (Exception ex)
            {
                // En caso de error, crear y devolver una respuesta de error
                var errorResponse = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el registro de AlertLog",
                };

                return errorResponse;
            }
        }

        public async Task<ResponseDTO> UpdateAlertLog(AlertLog alertLog)
        {
            try
            {
                await _alertLogRepository.UpdateAlertLog(alertLog);
                return new ResponseDTO { isSuccess = true };
            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error al actualizar el registro de AlertLog: {ex.Message}");
                return new ResponseDTO { isSuccess = false, message = "Error al actualizar el registro de AlertLog" };
            }
        }

        public async Task<ResponseDTO> DeleteAlertLog(int alertLogId)
        {
            try
            {
                await _alertLogRepository.DeleteAlertLog(alertLogId);
                return new ResponseDTO { isSuccess = true };
            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                Console.WriteLine($"Error al eliminar el registro de AlertLog: {ex.Message}");
                return new ResponseDTO { isSuccess = false, message = "Error al eliminar el registro de AlertLog" };
            }
        }

        public Task<List<AlertLog>> GetAllAlertLogsByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}