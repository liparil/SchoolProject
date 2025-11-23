using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbTeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;

        public DbTeacherRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        public void Delete(int teacherId)
        {
            var teacher = GetTeacherById(teacherId);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }
        }

        public List<Teacher> GetAllTeachers()
        {
            return _context.Teachers.ToList();
        }

        public Teacher GetTeacherById(int teacherId)
        {
            return _context.Teachers.FirstOrDefault(t => t.ID == teacherId);
        }

        public Teacher GetTeacherByNCode(string code)
        {
            return _context.Teachers.FirstOrDefault(t => t.NCode == code);
        }

        public void Update(Teacher teacher)
        {
            var existing = GetTeacherById(teacher.ID);
            if (existing != null)
            {
                existing.FullName = teacher.FullName;
                existing.NCode = teacher.NCode;
                existing.Expertise = teacher.Expertise;
                _context.SaveChanges();
            }
        }
    }
}
