using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IGetAllTeachers
    {
        Task<List<Teacher>> Execute();
    }
    public class GetAllTeachers(ITeacherRepository teacherRepository, ILogService logService ) : IGetAllTeachers
    {
        public async Task<List<Teacher>> Execute()
        {
            await logService.LogRead($"All Teachers fetched. Count: {teacherRepository.GetAllTeachers().Count}");
            return teacherRepository.GetAllTeachers();

        }
    }
}
