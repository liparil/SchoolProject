using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IUpdataTeacher
    {
        void Execute(Teacher teacher);
    }
    internal class UpdataTeacher : IUpdataTeacher
    {
        private readonly ITeacherRepository _teacherRepository;
        public UpdataTeacher(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public void Execute(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
        }
    }
}
