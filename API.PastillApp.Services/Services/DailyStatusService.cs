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
    public class DailyStatusService : IDailyStatusService
    {
        private readonly IDailyStatusRepository _dailyStatusRepository;
        private readonly IMapper _mapper;

        public DailyStatusService(IDailyStatusRepository dailyStatusRepository, IMapper mapper)
        {
            _dailyStatusRepository = dailyStatusRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateDailyStatus(CreateDailyStatusDTO dailyStatusDTO)
        {
            try
            {
                var newDailyStatus = _mapper.Map<DailyStatus>(dailyStatusDTO);
                await _dailyStatusRepository.AddDailyStatus(newDailyStatus);

                var response = new ResponseDTO
                {
                    isSuccess = true,
                    message = "Estado diario creado con éxito",
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear el estado diario",
                };

                Console.WriteLine($"Error al crear el estado diario: {ex.Message}");
                return errorResponse;
            }
        }

        public async Task<ResponseDTO> DeleteDailyStatus(int dailyStatusId)
        {
            try
            {
                await _dailyStatusRepository.DeleteDailyStatus(dailyStatusId);
                return new ResponseDTO { isSuccess = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el estado diario: {ex.Message}");
                return new ResponseDTO { isSuccess = false, message = "Error al eliminar el estado diario" };
            }
        }

        public async Task<List<DailyStatus>> GetDailyStatusByUserId(int userId)
        {
            try
            {
                return await _dailyStatusRepository.GetDailyStatusByUserId(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los estados diarios por UserId: {ex.Message}");
                throw;
            }
        }

        public async Task<DailyStatus> GetDailyStatusById(int dailyStatusId)
        {
            try
            {
                return await _dailyStatusRepository.GetDailyStatusById(dailyStatusId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el estado diario por ID: {ex.Message}");
                throw;
            }
        }

        public async Task<List<DailyStatus>> GetAllDailyStatuses()
        {
            try
            {
                return await _dailyStatusRepository.GetAllDailyStatuses();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los estados diarios: {ex.Message}");
                throw;
            }
        }

        public async Task<ResponseDTO> UpdateDailyStatus(UpdateDailyStatusDTO updateDailyStatusDTO)
        {
            try
            {
                var dailyStatusToUpdate = await _dailyStatusRepository.GetDailyStatusById(updateDailyStatusDTO.DailyStatusID);
                if (dailyStatusToUpdate == null)
                {
                    return new ResponseDTO
                    {
                        isSuccess = false,
                        message = "Estado diario no encontrado",
                    };
                }

                if(!string.IsNullOrWhiteSpace(updateDailyStatusDTO.Symptoms))
                {
                    dailyStatusToUpdate.Symptoms = updateDailyStatusDTO.Symptoms;
                }

                if (!string.IsNullOrWhiteSpace(updateDailyStatusDTO.Observation))
                {
                    dailyStatusToUpdate.Observation = updateDailyStatusDTO.Observation;
                }

                await _dailyStatusRepository.UpdateDailyStatus(dailyStatusToUpdate);

                return new ResponseDTO
                {
                    isSuccess = true,
                    message = "Estado diario actualizado",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el estado diario: {ex.Message}");
                return new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al actualizar el estado diario",
                };
            }
        }
    }
}