using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IGetCourseById
    {
        Course Execute(int courseId);
    }
    public class GetCourseById(AppDbContext context) : IGetCourseById
    {
      
        public Course Execute(int courseId)
        {
            return context.Courses.Include(c => c.teacher).FirstOrDefault(c => c.ID == courseId);
        }
    }
}
