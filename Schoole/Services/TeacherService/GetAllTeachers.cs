using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IGetAllTeachers
    {
        List<Teacher> Execute();
    }
    public class GetAllTeachers(ITeacherRepository teacherRepository) : IGetAllTeachers
    {
        public List<Teacher> Execute()
        {
            return teacherRepository.GetAllTeachers();
        }
    }
}
