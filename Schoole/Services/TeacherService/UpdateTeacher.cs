using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IUpdateTeacher
    {
        void Execute(Teacher teacher);
    }
    internal class UpdateTeacher(ITeacherRepository teacherRepository) : IUpdateTeacher
    {
        public void Execute(Teacher teacher)
        {
            teacherRepository.Update(teacher);
        }

    }
}
