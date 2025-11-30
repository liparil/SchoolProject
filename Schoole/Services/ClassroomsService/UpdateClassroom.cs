using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IUpdateClassroom
    {
        void Execute(Classroom classroom);
    }
    public class UpdateClassroom : IUpdateClassroom
    {
        private readonly IClassroomRepository _classroomRepository;
        public UpdateClassroom(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public void Execute(Classroom classroom)
        {
            _classroomRepository.Update(classroom);
        }
    }
}
