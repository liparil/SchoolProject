using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IUpdateCourse
    {
        void Execute(Course course);
    }
    public class UpdateCourse(ICourseRepository courseRepository) : IUpdateCourse
    {
        public void Execute(Course course)
        {
            courseRepository.Update(course);
        }
    }
}
