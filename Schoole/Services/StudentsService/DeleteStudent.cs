using Schoole.Interfaces;

namespace Schoole.Services.StudentsService
{
    public interface IDeleteStudent
    {
        void Execute(int studentId);
    }
    public class DeleteStudent(IStudentRepository studentRepository) : IDeleteStudent
    {
        public void Execute(int studentId)
        {
            studentRepository.Delete(studentId);
        }
    }
}
