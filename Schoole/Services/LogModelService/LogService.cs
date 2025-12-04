using Schoole.Interfaces;
using Schoole.Models;

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

        public async Task LogCreate(string message)
        {
            await WriteLog("Create", message);
        }
        public async Task LogRead(string message)
        {
            await WriteLog("Read", message);
        }
        public async Task LogDelete(string message)
        {
            await WriteLog("Delete", message);
        }
        public async Task LogUpdate(string message)
        {
            await WriteLog("Update", message);
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
