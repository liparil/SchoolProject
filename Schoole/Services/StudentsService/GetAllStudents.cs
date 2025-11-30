using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IGetAllStudents
    {
        List<Student> Execute();
    }
    public class GetAllStudents : IGetAllStudents
    {
        private readonly IStudentRepository _studentRepository;
        public GetAllStudents(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Student> Execute()
        {
            return _studentRepository.GetAllStudents();
        }
    }
}
