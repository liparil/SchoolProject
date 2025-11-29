using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IGetAllTeachersService
    {
        List<Teacher> Execute();
    }
    public class GetAllTeachersService : IGetAllTeachersService
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetAllTeachersService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public List<Teacher> Execute()
        {
            return _teacherRepository.GetAllTeachers();
        }
    }
}
