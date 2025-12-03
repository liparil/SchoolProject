using Schoole.Interfaces;
using Schoole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoole.Services.LogModelService
{
    public class LogService: ILogService
    {
        private readonly ILogModelRepository _logModelRepository;

        public LogService(ILogModelRepository logRepository)
        {
            _logModelRepository = logRepository;
        }

        public async Task LogInfo(string message)
        {
            await WriteLog("Info", message);
        }

        public async Task LogWarning(string message)
        {
            await WriteLog("Warning", message);
        }

        public async Task LogError(string message)
        {
            await WriteLog("Error", message);
        }

        private async Task WriteLog(string type, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            var log = new LogModel
            {
                LogType = type,
                Message = message,
                CreatedAt = DateTime.Now
            };

            await _logModelRepository.AddAsync(log);
        }

        public async Task<IEnumerable<LogModel>> GetAllLogs()
        {
            return await _logModelRepository.GetAllAsync();
        }
    }
}
