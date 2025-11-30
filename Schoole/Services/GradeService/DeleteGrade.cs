using Schoole.Interfaces;

namespace Schoole.Services.GradeService
{
    public interface IDeleteGrade
    {
        void Execute(int gradeId);
    }
    public class DeleteGrade : IDeleteGrade
    {
        private readonly IGradeRepository _gradeRepository;
        public DeleteGrade(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public void Execute(int gradeId)
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
