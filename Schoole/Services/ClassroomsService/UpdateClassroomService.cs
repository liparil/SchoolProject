using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IUpdateClassroomService
    {
        void Execute(Classroom classroom);
    }
    public class UpdateClassroomService : IUpdateClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        public UpdateClassroomService(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public void Execute(Classroom classroom)
        {
            _classroomRepository.Update(classroom);
        }
    }
}
