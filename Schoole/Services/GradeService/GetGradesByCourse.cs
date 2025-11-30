using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IGetGradesByCourse
    {
        List<Grade> Execute(int courseId);
    }
    public class GetGradesByCourse : IGetGradesByCourse
    {
        private readonly IGradeRepository _gradeRepository;
        public GetGradesByCourse(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public List<Grade> Execute(int courseId)
        {
            return _gradeRepository.GetGradeByCourseId(courseId);
        }
    }
}
