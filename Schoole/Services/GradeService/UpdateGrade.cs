using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IUpdateGradeService
    {
        void Execute(Grade grade);
    }
    public class UpdateGrade : IUpdateGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public UpdateGrade(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public void Execute(Grade grade)
        {
            _gradeRepository.Update(grade);
        }
    }
}
