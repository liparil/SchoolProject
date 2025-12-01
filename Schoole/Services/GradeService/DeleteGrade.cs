using Schoole.Interfaces;

namespace Schoole.Services.GradeService
{
    public interface IDeleteGrade
    {
        void Execute(int gradeId);
    }
    public class DeleteGrade (IGradeRepository gradeRepository): IDeleteGrade
    {
        public void Execute(int gradeId)
        {
            var grade = gradeRepository.GetGradeById(gradeId);
            if (grade != null)
            {

                grade.Student?.Grades.Remove(grade);
                gradeRepository.Delete(gradeId);
            }
        }
    }
}
