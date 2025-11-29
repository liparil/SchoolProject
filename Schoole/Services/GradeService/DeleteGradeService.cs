using Schoole.Interfaces;

namespace Schoole.Services.GradeService
{
    public interface IDeleteGradeService
    {
        void deleteGrade(int gradeId);
    }
    public class DeleteGradeService : IDeleteGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public DeleteGradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public void deleteGrade(int gradeId)
        {
            var grade = _gradeRepository.GetGradeById(gradeId);
            if (grade != null)
            {

                grade.Student?.Grades.Remove(grade);
                _gradeRepository.Delete(gradeId);
            }
        }
    }
}
