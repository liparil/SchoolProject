using Schoole.Interfaces;

namespace Schoole.Services.StudentsService
{
    public interface IDeleteStudent
    {
        void Execute(int studentId);
    }
    public class DeleteStudent : IDeleteStudent
    {
        private readonly IStudentRepository _studentRepository;
        public DeleteStudent(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Execute(int studentId)
        {
            _studentRepository.Delete(studentId);
        }
    }
}
