using API.PastillApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Repositories.Interface
{
    public interface IAlertLogRepository
    {
        Task AddAlertLog(AlertLog alertLog);

        // READ (Obtener un registro de AlertLog por ID)
        Task<AlertLog> GetAlertLogById(int alertLogId);

        // READ (Obtener todos los registros de AlertLog)
        Task<List<AlertLog>> GetAllAlertLogs();

        // UPDATE (Actualizar un registro de AlertLog)
        Task UpdateAlertLog(AlertLog alertLog);

        // DELETE (Eliminar un registro de AlertLog)
        Task DeleteAlertLog(int alertLogId);
    }
}