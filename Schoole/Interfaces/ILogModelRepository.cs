using Schoole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoole.Interfaces
{
    public interface ILogModelRepository
    {
        Task AddAsync(LogModel log);            
        
        Task<IEnumerable<LogModel>> GetAllAsync();   

        Task<LogModel> GetByIdAsync(int id);       
        
        Task<IEnumerable<LogModel>> GetByTypeAsync(string logType);
    }
}
