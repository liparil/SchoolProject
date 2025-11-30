using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IAddStudent
    {
        Output Execute(string name, DateTime birth, string nationalCode);
    }

    public class AddStudent : IAddStudent
    {
        private readonly IStudentRepository _studentRepository;


        public AddStudent(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Output Execute(string fullName, DateTime birthDate, string nCode)
        {
            var existing = _studentRepository.GetStudentByNcode(nCode);
            var student = new Student { FullName = fullName, BirthDate = birthDate, NCode = nCode };
            var output = new Output();
            if (existing != null)
            {
                output.Success = false;
                output.Message = "A student with this national code already exists!";
                return output;
            }

            _studentRepository.Add(student);
            output.Success = true;
            output.Message = "Student added successfully!";
            return output;
        }
    }
}
