using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IAddStudent
    {
        Output Execute(string name, DateTime birth, string nationalCode);
    }

    public class AddStudent(IStudentRepository studentRepository) : IAddStudent
    {
        public Output Execute(string fullName, DateTime birthDate, string nCode)
        {
            var existing = studentRepository.GetStudentByNcode(nCode);
            var student = new Student { FullName = fullName, BirthDate = birthDate, NCode = nCode };
            var output = new Output();
            if (existing != null)
            {
                output.Success = false;
                output.Message = "A student with this national code already exists!";
                return output;
            }

            studentRepository.Add(student);
            output.Success = true;
            output.Message = "Student added successfully!";
            return output;
        }
    }
}
