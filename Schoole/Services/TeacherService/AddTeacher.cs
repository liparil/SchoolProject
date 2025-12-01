using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IAddTeacher
    {
        Output Execute(string fullName, string nCode, string expertise);
    }
    public class AddTeacher(ITeacherRepository teacherRepository) : IAddTeacher
    {

        public Output Execute(string fullName, string nCode, string expertise)
        {
            var existing = teacherRepository.GetTeacherByNCode(nCode);
            var output = new Output();
            if (existing != null)
            {
                output.Success = false;
                output.Message = "A teacher with this national code already exists!";
                return output;
            }
            else
            {
                var teacher = new Teacher
                {
                    FullName = fullName,
                    Expertise = expertise,
                    NCode = nCode
                };

                teacherRepository.Add(teacher);
                output.Success = true;
                output.Message = "Teacher added successfully!";
                return output;
            }
        }
    }
}
