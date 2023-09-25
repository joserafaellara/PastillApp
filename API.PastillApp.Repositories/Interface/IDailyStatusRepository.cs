using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories.Interface
{
    public interface IDailyStatusRepository
    {
        Task AddDailyStatus(DailyStatus dailyStatus);

        Task<DailyStatus> GetDailyStatusById(int dailyStatusId);

        Task<List<DailyStatus>> GetAllDailyStatuses();

        Task<List<DailyStatus>> GetDailyStatusByUserId(int userId);

        Task UpdateDailyStatus(DailyStatus dailyStatus);

        Task DeleteDailyStatus(int dailyStatusId);
    }
}