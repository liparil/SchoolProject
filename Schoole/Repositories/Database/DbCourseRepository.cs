using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbCourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public DbCourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void Delete(int courseId)
        {
            var course = GetCourseById(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }

        public List<Course> GetAll()
        {
            return _context.Courses.Include(c => c.teacher).ToList();

        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Include(c => c.teacher).FirstOrDefault(c => c.ID == courseId);
        }

        public List<Course> GetCourseByTeacherId(int teacherId)
        {
            return _context.Courses.Where(c => c.teacher != null && c.teacher.ID == teacherId).ToList();
        }

        public void Update(Course course)
        {
            var existing = GetCourseById(course.ID);
            if (existing != null)
            {
                existing.Title = course.Title;
                existing.teacher = course.teacher;
                _context.SaveChanges();

            }
        }
    }
}
