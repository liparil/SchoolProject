using Schoole.Interfaces;
using Schoole.Models;
using Schoole.Repositories.Database;

namespace Schoole.Services.ClassroomsService
{
    public interface IDeleteClassroom
    {
        Task Execute(int classroomId);
    }
    public class DeleteClassroom(IClassroomRepository classroomRepository, ILogService logService) : IDeleteClassroom
    {
        public async Task Execute(int classroomId)
        {
            var classroom = classroomRepository.GetClassroomById(classroomId);
            if (classroom == null) return;

            classroomRepository.Delete(classroomId);
            await logService.LogDelete($"Classroom deleted: {classroom.Name}");
        }
    }
}
