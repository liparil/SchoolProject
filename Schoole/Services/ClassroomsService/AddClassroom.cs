using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IAddClassroom
    {
        Output Execute(string name);
    }
    public class AddClassroom : IAddClassroom
    {
        private readonly IClassroomRepository _classroomRepository;

        public AddClassroom(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public Output Execute(string name)
        {
            var classroom = new Classroom { Name = name };
            var output = new Output();

            _classroomRepository.Add(classroom);

            output.Success = true;
            output.Message = $"{name} Added Sucssesfuly.";
            return output;
        }
    }
}
