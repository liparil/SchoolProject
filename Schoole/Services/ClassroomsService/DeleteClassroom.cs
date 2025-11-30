using Schoole.Interfaces;

namespace Schoole.Services.ClassroomsService
{
    public interface IDeleteClassroom
    {
        void Execute(int classroomId);
    }
    public class DeleteClassroom : IDeleteClassroom
    {
        private readonly IClassroomRepository _classroomRepository;
        public DeleteClassroom(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public void Execute(int classroomId)
        {
            _classroomRepository.Delete(classroomId);
        }
    }
}
