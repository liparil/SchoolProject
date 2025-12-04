using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IGetAllClassrooms
    {
        Task<List<Classroom>> Execute();
    }
    public class GetAllClassrooms(IClassroomRepository classroomRepository, ILogService logService) : IGetAllClassrooms
    {
        public async Task<List<Classroom>> Execute()
        {
            await logService.LogRead($"All Classroom fetched. Count: {classroomRepository.GetAllClassrooms().Count}");
            return classroomRepository.GetAllClassrooms();
        }
    }
}
