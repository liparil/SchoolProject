using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IGetAllCourses
    {
        Task<List<Course>> Execute();
    }
    public class GetAllCourses(ICourseRepository courseRepository, ILogService logService) : IGetAllCourses
    {

        public async Task<List<Course>> Execute()
        {
            await logService.LogRead($"All Course fetched. Count: {courseRepository.GetAll().Count}");
            return courseRepository.GetAll();
        }
    }
}
