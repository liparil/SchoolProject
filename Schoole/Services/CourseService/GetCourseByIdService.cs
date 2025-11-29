using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IGetCourseByIdService
    {
        Course Execute(int courseId);
    }
    public class GetCourseByIdService : IGetCourseByIdService
    {
        private readonly AppDbContext _context;
        public GetCourseByIdService(AppDbContext context)
        {
            _context = context;
        }

        public Course Execute(int courseId)
        {
            return _context.Courses.Include(c => c.teacher).FirstOrDefault(c => c.ID == courseId);
        }
    }
}
