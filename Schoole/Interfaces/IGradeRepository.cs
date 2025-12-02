using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface IGradeRepository
    {
        void Add(Grade grade);

        void Update(Grade grade);

        void Delete(int gradeId);

        Grade GetGradeById(int gradeId);

        List<Grade> GetAllGrades();

        List<Grade> GetGradeByStudentId(int studentId);

        List<Grade> GetGradeByCourseId(int courseId);
    }
}
