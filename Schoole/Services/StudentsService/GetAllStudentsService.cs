using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IGetAllStudentsService
    {
        List<Student> Execute();
    }
    public class GetAllStudentsService : IGetAllStudentsService
    {
        private readonly IStudentRepository _studentRepository;
        public GetAllStudentsService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Student> Execute()
        {
            return _studentRepository.GetAllStudents();
        }
    }
}
