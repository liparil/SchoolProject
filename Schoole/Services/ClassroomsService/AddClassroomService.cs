using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IAddClassroomService
    {
        Output Execute(string name);
    }
    public class AddClassroomService : IAddClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;

        public AddClassroomService(IClassroomRepository classroomRepository)
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
