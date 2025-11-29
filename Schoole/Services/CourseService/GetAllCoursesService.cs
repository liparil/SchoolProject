using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IGetAllCoursesService
    {
        List<Course> Execute();
    }
    public class GetAllCoursesService : IGetAllCoursesService
    {
        private readonly ICourseRepository _courseRepository;

        public GetAllCoursesService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> Execute()
        {
            return _courseRepository.GetAll();
        }
    }
}
