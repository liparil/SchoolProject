using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbClassroomRepository(AppDbContext context) : IClassroomRepository
    {
        public void Add(Classroom classroom)
        {
            context.Classrooms.Add(classroom);
            context.SaveChanges();
        }

        public void Delete(int classroomId)
        {
            var classroom = GetClassroomById(classroomId);
            if (classroom != null)
            {
                context.Classrooms.Remove(classroom);
                context.SaveChanges();
            }
        }

        public List<Classroom> GetAllClassrooms()
        {
            return context.Classrooms
            .Include(c => c.Students)
            .ToList();
        }

        public Classroom GetClassroomById(int classroomId)
        {
            return context.Classrooms.Include(c => c.Students).FirstOrDefault(c => c.ID == classroomId);
        }

        public Classroom GetClassroomByName(string name)
        {
            return context.Classrooms.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(Classroom classroom)
        {
            var existing = GetClassroomById(classroom.ID);
            if (existing != null)
            {
                existing.Name = classroom.Name;
                existing.Students = classroom.Students;
                existing.Courses = classroom.Courses;
                context.SaveChanges();
            }
        }
    }
}
