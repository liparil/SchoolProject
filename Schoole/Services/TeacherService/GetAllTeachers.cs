using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IGetAllTeachers
    {
        List<Teacher> Execute();
    }
    public class GetAllTeachers : IGetAllTeachers
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetAllTeachers(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public List<Teacher> Execute()
        {
            return _teacherRepository.GetAllTeachers();
        }
    }
}
