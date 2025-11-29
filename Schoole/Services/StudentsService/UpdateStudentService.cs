using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IUpdateStudentService
    {
        void Execute(Student student);
    }
    public class UpdateStudentService : IUpdateStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Execute(Student student)
        {
            _studentRepository.Update(student);
        }
    }
}
