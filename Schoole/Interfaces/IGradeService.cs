using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface IGradeService
    {
        //(bool Success, string Message) AddGrade(int studentId, int courseId, double score);
        OutputTest AddGrade(int studentId, int courseId, double score);
        void UpdateGrade(Grade grade);
        void deleteGrade(int gradeId);
        List<Grade> GetGradesByStudent(int studentId);
        List<Grade> GetGradesByCourse(int courseId);
        double CalculateAverage(int studentId);

    }
}
