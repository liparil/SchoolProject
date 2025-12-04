using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IUpdateClassroom
    {
        Task Execute(Classroom classroom);
    }
    public class UpdateClassroom(IClassroomRepository classroomRepository, ILogService logService) : IUpdateClassroom
    {
        public async Task Execute(Classroom classroom)
        {
            classroomRepository.Update(classroom);
            await logService.LogUpdate($"Classroom updated: {classroom.Name}");
        }
    }
}
