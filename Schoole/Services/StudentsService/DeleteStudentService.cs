using Schoole.Interfaces;

namespace Schoole.Services.StudentsService
{
    public interface IDeleteStudentService
    {
        void Execute(int studentId);
    }
    public class DeleteStudentService : IDeleteStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public DeleteStudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Execute(int studentId)
        {
            _studentRepository.Delete(studentId);
        }
    }
}
