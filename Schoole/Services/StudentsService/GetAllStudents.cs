using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IGetAllStudents
    {
        List<Student> Execute();
    }
    public class GetAllStudents(IStudentRepository studentRepository) : IGetAllStudents
    {
        public List<Student> Execute()
        {
            return studentRepository.GetAllStudents();
        }
    }
}
