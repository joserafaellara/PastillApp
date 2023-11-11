using API.PastillApp.Domain.Entities;
using API.PastillApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Services.Interfaces
{
    public interface IDailyStatusService
    {
        Task<ResponseDTO> CreateDailyStatus(CreateDailyStatusDTO dailyStatusDTO);
        Task<List<DailyStatus>> GetDailyStatusByUserId(int userId);
        Task<DailyStatus> GetDailyStatusById(int dailyStatusId);
        Task<List<DailyStatus>> GetAllDailyStatuses();
        Task<ResponseDTO> UpdateDailyStatus(UpdateDailyStatusDTO dailyStatus);
        Task<ResponseDTO> DeleteDailyStatus(int dailyStatusId);
        Task<DailyStatusDTO> GetDailyStatusByDateAndID(int userId, string dateString);
        Task<ResponseDTO> UpdateStatus(int userId, UpdateDailyStatusDTO updateDailyStatusDTO);
    }
}