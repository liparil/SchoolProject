using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoole.Repositories.Database
{
    public class DbLogModelRepository(AppDbContext context) : ILogModelRepository
    {
        public async Task AddAsync(LogModel log)
        {
            await context.LogModels.AddAsync(log);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LogModel>> GetAllAsync()
       => await context.LogModels.ToListAsync();

        public async Task<LogModel> GetByIdAsync(int id)
         => await context.LogModels.FindAsync(id);


        public async Task<IEnumerable<LogModel>> GetByTypeAsync(string logType)
            => await context.LogModels.Where(l => l.LogType == logType).ToListAsync();
    }
}
