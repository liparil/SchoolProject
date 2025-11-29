using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IUpdateGradeService
    {
        void UpdateGrade(Grade grade);
    }
    public class UpdateGradeService : IUpdateGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public UpdateGradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public void UpdateGrade(Grade grade)
        {
            _gradeRepository.Update(grade);
        }
    }
}
