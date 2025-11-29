using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface ISearchTeacherByNationalCodeService
    {
        Teacher Execute(string nCode);
    }
    public class SearchTeacherByNationalCodeService : ISearchTeacherByNationalCodeService
    {
        private readonly AppDbContext _context;
        public SearchTeacherByNationalCodeService(AppDbContext context)
        {
            _context = context;
        }
        public Teacher Execute(string nCode)
        {
            return _context.Teachers.FirstOrDefault(t => t.NCode == nCode);
        }
    }

}
