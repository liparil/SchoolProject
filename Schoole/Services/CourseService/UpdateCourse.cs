using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IUpdateCourse
    {
        Task Execute(Course course);
    }
    public class UpdateCourse(ICourseRepository courseRepository, ILogService logService) : IUpdateCourse
    {
        public async Task Execute(Course course)
        {
            courseRepository.Update(course);
            await logService.LogInfo($"Course updated: {course.Title}");
        }
    }
}
