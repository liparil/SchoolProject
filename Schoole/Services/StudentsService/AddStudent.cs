using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IAddStudent
    {
        Task<Output> Execute(string name, DateTime birth, string nationalCode);
    }

    public class AddStudent(IStudentRepository studentRepository, ILogService logService) : IAddStudent
    {
        public async Task<Output> Execute(string fullName, DateTime birthDate, string nCode)
        {
            var existing = studentRepository.GetStudentByNcode(nCode);
            var student = new Student { FullName = fullName, BirthDate = birthDate, NCode = nCode };
            var output = new Output();
            if (existing != null)
            {
                output.Success = false;
                output.Message = "A student with this national code already exists!";
                await logService.LogWarning($"Duplicate student creation attempt: {fullName} - {nCode}");
                return output;
            }

            studentRepository.Add(student);
            output.Success = true;
            output.Message = "Student added successfully!";
            await logService.LogCreate($"Student added: {fullName} - {nCode}");
            return output;
        }
    }
}
