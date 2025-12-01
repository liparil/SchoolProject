using Schoole.Interfaces;

namespace Schoole.Services.ClassroomsService
{
    public interface IDeleteClassroom
    {
        void Execute(int classroomId);
    }
    public class DeleteClassroom(IClassroomRepository classroomRepository) : IDeleteClassroom
    {
        public void Execute(int classroomId)
        {
            classroomRepository.Delete(classroomId);
        }
    }
}
