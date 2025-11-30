using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IUpdateCourse
    {
        void Execute(Course course);
    }
    public class UpdateCourse : IUpdateCourse
    {
        private readonly ICourseRepository _courseRepository;
        public UpdateCourse(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public void Execute(Course course)
        {
            _courseRepository.Update(course);
        }
    }
}
