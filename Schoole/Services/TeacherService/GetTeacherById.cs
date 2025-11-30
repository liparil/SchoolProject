using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IGetTeacherById
    {
        Teacher Execute(int teacherId);
    }
    public class GetTeacherById : IGetTeacherById
    {
        private readonly AppDbContext _context;
        public GetTeacherById(AppDbContext context)
        {
            _context = context;
        }

        public Teacher Execute(int teacherId)
        {
            return _context.Teachers.FirstOrDefault(t => t.ID == teacherId);
        }
    }
}
