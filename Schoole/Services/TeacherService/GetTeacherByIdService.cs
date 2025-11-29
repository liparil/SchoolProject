using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IGetTeacherByIdService
    {
        Teacher Execute(int teacherId);
    }
    public class GetTeacherByIdService : IGetTeacherByIdService
    {
        private readonly AppDbContext _context;
        public GetTeacherByIdService(AppDbContext context)
        {
            _context = context;
        }

        public Teacher Execute(int teacherId)
        {
            return _context.Teachers.FirstOrDefault(t => t.ID == teacherId);
        }
    }
}
