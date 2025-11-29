using Schoole.Interfaces;

namespace Schoole.Services.ClassroomsService
{
    public interface IDeleteClassroomService
    {
        void Execute(int classroomId);
    }
    public class DeleteClassroomService : IDeleteClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        public DeleteClassroomService(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public void Execute(int classroomId)
        {
            _classroomRepository.Delete(classroomId);
        }
    }
}
