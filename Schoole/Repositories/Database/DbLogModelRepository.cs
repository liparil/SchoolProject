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
    public class DbLogModelRepository : ILogModelRepository
    {
        private readonly AppDbContext _context;

        public DbLogModelRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LogModel log)
        {
            await _context.LogModels.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LogModel>> GetAllAsync()
       => await _context.LogModels.ToListAsync();

        public async Task<LogModel> GetByIdAsync(int id)
         => await _context.LogModels.FindAsync(id);


        public async Task<IEnumerable<LogModel>> GetByTypeAsync(string logType)
            => await _context.LogModels.Where(l => l.LogType == logType).ToListAsync();
    }
}
