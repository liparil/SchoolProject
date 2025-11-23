using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface ITeacherRepository
    {
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(int teacherId);
        Teacher GetTeacherById(int teacherId);
        Teacher GetTeacherByNCode(string code);
        List<Teacher> GetAllTeachers();
    }
}
