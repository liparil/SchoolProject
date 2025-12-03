using Schoole.Interfaces;
using Schoole.Models;
using Schoole.Services.LogModelService;

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
            await logService.LogInfo($"Classroom updated: {classroom.Name}");
        }
    }
}
