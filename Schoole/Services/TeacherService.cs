using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public (bool Success, string Message) AddTeacher(string fullName, string nCode, string expertise)
        {
            var existing = _teacherRepository.GetTeacherByNCode(nCode);
            if (existing != null)
            {
                return (false, "A teacher with this national code already exists!");
            }
            else
            {
                var teacher = new Teacher
                {
                    FullName = fullName,
                    Expertise = expertise,
                    NCode = nCode
                };

                _teacherRepository.Add(teacher);
                return (true, "Teacher added successfully!");
            }
        }

        public void DeleteTeacher(int teacherId)
        {
            _teacherRepository.Delete(teacherId);
        }

        public List<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAllTeachers();
        }

        public void UpdataTeacher(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
        }
        public (bool Success, string Message) ShowTeacher(string nCode)
        {
            var existing = _teacherRepository.GetTeacherByNCode(nCode);
            if (existing == null)
            {
                return (false, "No teacher with this national code was found.");
            }
            else
            {
                return (true, $"Teacher successfully found.\nName: {existing.FullName}\nNational code: {existing.NCode}\nExpertise: {existing.Expertise}");
            }
        }
    }
}
