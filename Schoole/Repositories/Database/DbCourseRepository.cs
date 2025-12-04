using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbCourseRepository(AppDbContext context) : ICourseRepository
    {
        public void Add(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Delete(int courseId)
        {
            var course = GetCourseById(courseId);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
            }
        }

        public List<Course> GetAll()
        {
            return context.Courses.Include(c => c.teacher).ToList();

        }

        public Course GetCourseById(int courseId)
        {
            return context.Courses.Include(c => c.teacher).FirstOrDefault(c => c.ID == courseId);
        }

        public List<Course> GetCourseByTeacherId(int teacherId)
        {
            return context.Courses.Where(c => c.teacher != null && c.teacher.ID == teacherId).ToList();
        }

        public void Update(Course course)
        {
            var existing = GetCourseById(course.ID);
            if (existing != null)
            {
                existing.Title = course.Title;
                existing.teacher = course.teacher;
                context.SaveChanges();

            }
        }
    }
}
