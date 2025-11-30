using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IGetAllCourses
    {
        List<Course> Execute();
    }
    public class GetAllCourses : IGetAllCourses
    {
        private readonly ICourseRepository _courseRepository;

        public GetAllCourses(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> Execute()
        {
            return _courseRepository.GetAll();
        }
    }
}
