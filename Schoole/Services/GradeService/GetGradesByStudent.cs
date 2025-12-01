using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IGetGradesByStudent
    {
        List<Grade> Execute(int studentId);
    }
    public class GetGradesByStudent(IGradeRepository gradeRepository) : IGetGradesByStudent
    {
        public List<Grade> Execute(int studentId)
        {
            return gradeRepository.GetGradeByStudentId(studentId);
        }
    }
}
