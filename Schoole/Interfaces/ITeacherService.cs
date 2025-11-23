using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface ITeacherService
    {
        (bool Success, string Message) AddTeacher(string fullName,string nCode,string expertise);
        void UpdataTeacher (Teacher teacher);
        void DeleteTeacher (int teacherId);
        (bool Success, string Message) ShowTeacher(string nCode);
        List<Teacher> GetAllTeachers ();
        
    }
}
