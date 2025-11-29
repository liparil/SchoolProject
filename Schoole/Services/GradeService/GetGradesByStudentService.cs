using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IGetGradesByStudentService
    {
        List<Grade> GetGradesByStudent(int studentId);
    }
    public class GetGradesByStudentService : IGetGradesByStudentService
    {
        private readonly IGradeRepository _gradeRepository;
        public GetGradesByStudentService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public List<Grade> GetGradesByStudent(int studentId)
        {
            return _gradeRepository.GetGradeByStudentId(studentId);
        }
    }
}
