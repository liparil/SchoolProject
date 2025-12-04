using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbStudentRepository(AppDbContext context) : IStudentRepository
    {

        public void Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }

        public void Delete(int studentId)
        {
            var student = GetStudentById(studentId);
            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        public List<Student> GetAllStudents()
        {
            return context.Students.ToList();
        }

        public Student GetStudentById(int studentId)
        {
            return context.Students.Include(s => s.Grades).ThenInclude(g => g.Course).FirstOrDefault(s => s.ID == studentId);
        }

        public Student GetStudentByNcode(string nCode)
        {
            return context.Students.FirstOrDefault(s => s.NCode == nCode);
        }

        public void save()
        {
            context.SaveChanges();
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
                context.SaveChanges();
            }
        }
    }
}
