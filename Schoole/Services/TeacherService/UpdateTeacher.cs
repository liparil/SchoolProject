using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IUpdateTeacher
    {
        Task Execute(Teacher teacher);
    }
    internal class UpdateTeacher(ITeacherRepository teacherRepository, ILogService logService) : IUpdateTeacher
    {
        public async Task Execute(Teacher teacher)
        {
            teacherRepository.Update(teacher);
            await logService.LogUpdate($"Teacher updated: {teacher.FullName} - {teacher.NCode}");
        }

    }
}
