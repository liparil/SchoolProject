using Schoole.Data;
using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Repositories.Database
{
    public class DbGradeRepository(AppDbContext context) : IGradeRepository
    {
        public void Add(Grade grade)
        {
            context.Grades.Add(grade);
            context.SaveChanges();
        }

        public void Delete(int gradeId)
        {
            var grade = GetGradeById(gradeId);
            if (grade != null)
            {
                context.Grades.Remove(grade);
                context.SaveChanges();
            }
        }

        public List<Grade> GetAllGrades()
        {
            return context.Grades.ToList();
        }

        public List<Grade> GetGradeByCourseId(int courseId)
        {
            return context.Grades.Where(g => g.Course != null && g.Course.ID == courseId).ToList();
        }

        public Grade GetGradeById(int gradeId)
        {
            return context.Grades.FirstOrDefault(g => g.ID == gradeId);
        }

        public List<Grade> GetGradeByStudentId(int studentId)
        {
            return context.Grades.Where(g => g.Student != null && g.Student.ID == studentId).ToList();
        }

        public void Update(Grade grade)
        {
            var existing = GetGradeById(grade.ID);
            if (existing != null)
            {
                existing.Score = grade.Score;
                existing.Student = grade.Student;
                existing.Course = grade.Course;
                context.SaveChanges();

            }
        }
    }
}
