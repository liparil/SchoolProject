using Schoole.Interfaces;

namespace Schoole.Services.CourseService
{
    public interface IDeleteCourseServise
    {
        void Execute(int courseId);
    }
    public class DeleteCourseService : IDeleteCourseServise
    {
        private readonly ICourseRepository _courseRepository;
        public DeleteCourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Execute(int courseId)
        {
            var course = _courseRepository.GetCourseById(courseId);

            if (course != null && course.teacher != null)
            {
                course.teacher.Courses.Remove(course);
            }

            _courseRepository.Delete(courseId);
        }
    }
}
