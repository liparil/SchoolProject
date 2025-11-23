using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbClassroomRepository : IClassroomRepository
    {
        private readonly AppDbContext _context;

        public DbClassroomRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Classroom classroom)
        {
            _context.Classrooms.Add(classroom);
            _context.SaveChanges();
        }

        public void Delete(int classroomId)
        {
            var classroom = GetClassroomById(classroomId);
            if (classroom != null)
            {
                _context.Classrooms.Remove(classroom);
                _context.SaveChanges();
            }
        }

        public List<Classroom> GetAllClassrooms()
        {
            return _context.Classrooms
            .Include(c => c.Students)
            .ToList();
        }

        public Classroom GetClassroomById(int classroomId)
        {
            return _context.Classrooms.Include(c => c.Students).FirstOrDefault(c => c.ID == classroomId);
        }

        public Classroom GetClassroomByName(string name)
        {
            return _context.Classrooms.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(Classroom classroom)
        {
            var existing = GetClassroomById(classroom.ID);
            if (existing != null)
            {
                existing.Name = classroom.Name;
                existing.Students = classroom.Students;
                existing.Courses = classroom.Courses;
                _context.SaveChanges();
            }
        }
    }
}
