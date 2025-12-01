using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IUpdateGradeService
    {
        void Execute(Grade grade);
    }
    public class UpdateGrade(IGradeRepository gradeRepository) : IUpdateGradeService
    {
        public void Execute(Grade grade)
        {
            gradeRepository.Update(grade);
        }
    }
}
