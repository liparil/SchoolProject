using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IGetAllClassrooms
    {
        List<Classroom> Execute();
    }
    public class GetAllClassrooms : IGetAllClassrooms
    {
        private readonly IClassroomRepository _classroomRepository;

        public GetAllClassrooms(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public List<Classroom> Execute()
        {
            return _classroomRepository.GetAllClassrooms();
        }
    }
}
