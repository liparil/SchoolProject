using Schoole.Interfaces;

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
            await logService.LogDelete($"teacher deleted: {teacher.FullName} - {teacher.NCode}");
        }
    }
}
