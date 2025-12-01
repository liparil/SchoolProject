using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IGetAllClassrooms
    {
        List<Classroom> Execute();
    }
    public class GetAllClassrooms(IClassroomRepository classroomRepository) : IGetAllClassrooms
    {
        public List<Classroom> Execute()
        {
            return classroomRepository.GetAllClassrooms();
        }
    }
}
