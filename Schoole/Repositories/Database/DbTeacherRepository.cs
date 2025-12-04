using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbTeacherRepository(AppDbContext context) : ITeacherRepository
    {
        public void Add(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            context.SaveChanges();
        }

        public void Delete(int teacherId)
        {
            var teacher = GetTeacherById(teacherId);
            if (teacher != null)
            {
                context.Teachers.Remove(teacher);
                context.SaveChanges();
            }
        }

        public List<Teacher> GetAllTeachers()
        {
            return context.Teachers.ToList();
        }

        public Teacher GetTeacherById(int teacherId)
        {
            return context.Teachers.FirstOrDefault(t => t.ID == teacherId);
        }

        public Teacher GetTeacherByNCode(string code)
        {
            return context.Teachers.FirstOrDefault(t => t.NCode == code);
        }

        public void Update(Teacher teacher)
        {
            var existing = GetTeacherById(teacher.ID);
            if (existing != null)
            {
                existing.FullName = teacher.FullName;
                existing.NCode = teacher.NCode;
                existing.Expertise = teacher.Expertise;
                context.SaveChanges();
            }
        }
    }
}
