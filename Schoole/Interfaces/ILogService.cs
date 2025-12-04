using Schoole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoole.Interfaces
{
    public interface ILogService
    {
        Task LogInfo(string message);
        Task LogCreate(string message);
        Task LogDelete(string message);
        Task LogUpdate(string message);
        Task LogRead(string message);
        Task LogWarning(string message);
        Task LogError(string message);
        Task<IEnumerable<LogModel>> GetAllLogs();
    }
}
