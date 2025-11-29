using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IUpdateCourseService
    {
        void Execute(Course course);
    }
    public class UpdateCourseService : IUpdateCourseService
    {
        private readonly ICourseRepository _courseRepository;
        public UpdateCourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public void Execute(Course course)
        {
            _courseRepository.Update(course);
        }
    }
}
