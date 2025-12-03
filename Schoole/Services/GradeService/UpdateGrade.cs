using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IUpdateGradeService
    {
        Task Execute(Grade grade);
    }
    public class UpdateGrade(IGradeRepository gradeRepository, ILogService logService) : IUpdateGradeService
    {
        public async Task Execute(Grade grade)
        {
            gradeRepository.Update(grade);
            //await logService.LogInfo($"Grade updated: {grade.} - {student.NCode}");
        }
    }
}
