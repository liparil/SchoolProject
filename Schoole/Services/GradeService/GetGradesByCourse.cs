using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IGetGradesByCourse
    {
        List<Grade> Execute(int courseId);
    }
    public class GetGradesByCourse(IGradeRepository gradeRepository) : IGetGradesByCourse
    {
        public List<Grade> Execute(int courseId)
        {
            return gradeRepository.GetGradeByCourseId(courseId);
        }
    }
}
