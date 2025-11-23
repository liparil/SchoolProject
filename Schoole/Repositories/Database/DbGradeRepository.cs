using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbGradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;

        public DbGradeRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Grade grade)
        {
            _context.Grades.Add(grade);
            _context.SaveChanges();
        }

        public void Delete(int gradeId)
        {
            var grade = GetGradeById(gradeId);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                _context.SaveChanges();
            }
        }

        public List<Grade> GetAllGrades()
        {
            return _context.Grades.ToList();
        }

        public List<Grade> GetGradeByCourseId(int courseId)
        {
            return _context.Grades.Where(g => g.Course != null && g.Course.ID == courseId).ToList();
        }

        public Grade GetGradeById(int gradeId)
        {
            return _context.Grades.FirstOrDefault(g => g.ID == gradeId);
        }

        public List<Grade> GetGradeByStudentId(int studentId)
        {
            return _context.Grades.Where(g => g.Student != null && g.Student.ID == studentId).ToList();
        }

        public void Update(Grade grade)
        {
            var existing = GetGradeById(grade.ID);
            if (existing != null)
            {
                existing.Score = grade.Score;
                existing.Student = grade.Student;
                existing.Course = grade.Course;
                _context.SaveChanges();

            }
        }
    }
}
