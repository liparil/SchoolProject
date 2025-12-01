using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IGetAllCourses
    {
        List<Course> Execute();
    }
    public class GetAllCourses(ICourseRepository courseRepository) : IGetAllCourses
    {

        public List<Course> Execute()
        {
            return courseRepository.GetAll();
        }
    }
}
