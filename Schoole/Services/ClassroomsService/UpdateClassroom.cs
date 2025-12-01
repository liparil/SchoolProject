using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IUpdateClassroom
    {
        void Execute(Classroom classroom);
    }
    public class UpdateClassroom(IClassroomRepository classroomRepository) : IUpdateClassroom
    {
        public void Execute(Classroom classroom)
        {
            classroomRepository.Update(classroom);
        }
    }
}
