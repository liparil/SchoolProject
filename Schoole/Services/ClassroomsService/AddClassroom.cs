using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IAddClassroom
    {
        Output Execute(string name);
    }
    public class AddClassroom(IClassroomRepository classroomRepository) : IAddClassroom
    {
        public Output Execute(string name)
        {
            var classroom = new Classroom { Name = name };
            var output = new Output();

            classroomRepository.Add(classroom);

            output.Success = true;
            output.Message = $"{name} Added Sucssesfuly.";
            return output;
        }
    }
}
