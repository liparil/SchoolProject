using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IAddTeacher
    {
        Task<Output> Execute(string fullName, string nCode, string expertise);
    }
    public class AddTeacher(ITeacherRepository teacherRepository, ILogService logService) : IAddTeacher
    {

        public async Task<Output> Execute(string fullName, string nCode, string expertise)
        {
            var existing = teacherRepository.GetTeacherByNCode(nCode);
            var teacher = new Teacher { FullName = fullName, Expertise = expertise, NCode = nCode };
            var output = new Output();
            if (existing != null)
            {
                output.Success = false;
                output.Message = "A teacher with this national code already exists!";
                await logService.LogWarning($"Duplicate teacher creation attempt: {fullName} - {nCode}");
                return output;
            }

                teacherRepository.Add(teacher);
                output.Success = true;
                output.Message = "Teacher added successfully!";
                await logService.LogInfo($"Teacher added: {fullName} - {nCode}");
                return output;
        }
    }
}
