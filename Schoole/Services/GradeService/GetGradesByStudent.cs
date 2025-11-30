using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IGetGradesByStudent
    {
        List<Grade> Execute(int studentId);
    }
    public class GetGradesByStudent : IGetGradesByStudent
    {
        private readonly IGradeRepository _gradeRepository;
        public GetGradesByStudent(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public List<Grade> Execute(int studentId)
        {
            return _gradeRepository.GetGradeByStudentId(studentId);
        }
    }
}
