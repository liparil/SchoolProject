using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IUpdataTeacherService
    {
        void Execute(Teacher teacher);
    }
    internal class UpdataTeacherService : IUpdataTeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public UpdataTeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public void Execute(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
        }
    }
}
