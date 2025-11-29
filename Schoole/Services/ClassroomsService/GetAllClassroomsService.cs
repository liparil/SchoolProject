using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IGetAllClassroomsService
    {
        List<Classroom> Execute();
    }
    public class GetAllClassroomsService : IGetAllClassroomsService
    {
        private readonly IClassroomRepository _classroomRepository;

        public GetAllClassroomsService(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public List<Classroom> Execute()
        {
            return _classroomRepository.GetAllClassrooms();
        }
    }
}
