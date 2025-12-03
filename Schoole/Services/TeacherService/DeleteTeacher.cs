using Schoole.Interfaces;
using Schoole.Models;
using Schoole.Repositories.Database;

namespace Schoole.Services.TeacherService
{
    public interface IDeleteTeacher
    {
        Task Execute(int studentId);
    }
    public class DeleteTeacher(ITeacherRepository teacherRepository, ILogService logService) : IDeleteTeacher
    {
        public async Task Execute(int teacherId)
        {
            var teacher = teacherRepository.GetTeacherById(teacherId);
            if (teacher == null) return;

            teacherRepository.Delete(teacherId);
            await logService.LogInfo($"teacher deleted: {teacher.FullName} - {teacher.NCode}");
        }
    }
}
