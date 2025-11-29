using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IGetGradesByCourseService
    {
        List<Grade> GetGradesByCourse(int courseId);
    }
    public class GetGradesByCourseService : IGetGradesByCourseService
    {
        private readonly IGradeRepository _gradeRepository;
        public GetGradesByCourseService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public List<Grade> GetGradesByCourse(int courseId)
        {
            return _gradeRepository.GetGradeByCourseId(courseId);
        }
    }
}
