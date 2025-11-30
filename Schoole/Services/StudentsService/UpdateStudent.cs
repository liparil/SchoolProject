using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IUpdateStudent
    {
        void Execute(Student student);
    }
    public class UpdateStudent : IUpdateStudent
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudent(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Execute(Student student)
        {
            _studentRepository.Update(student);
        }
    }
}
