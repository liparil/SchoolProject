using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface ISearchByNationalCodeService
    {
        Student Execute(string nCode);
    }
    public class SearchByNationalCodeService : ISearchByNationalCodeService
    {
        private readonly AppDbContext _appDbContext;
        public SearchByNationalCodeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Student Execute(string nCode)
        {
            return _appDbContext.Students.FirstOrDefault(s => s.NCode == nCode);
        }
    }
}
