using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbStudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public DbStudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Delete(int studentId)
        {
            var student = GetStudentById(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student GetStudentById(int studentId)
        {
            return _context.Students.Include(s => s.Grades).ThenInclude(g => g.Course).FirstOrDefault(s => s.ID == studentId);
        }

        public Student GetStudentByNcode(string nCode)
        {
            return _context.Students.FirstOrDefault(s => s.NCode == nCode);
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            var existing = GetStudentById(student.ID);
            if (existing != null)
            {
                existing.FullName = student.FullName;
                existing.NCode = student.NCode;
                existing.BirthDate = student.BirthDate;
                existing.Classroom = student.Classroom;
                _context.SaveChanges();
            }
        }
    }
}
