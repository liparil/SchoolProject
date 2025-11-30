using Schoole.Interfaces;

namespace Schoole.Services.CourseService
{
    public interface IDeleteCourse
    {
        void Execute(int courseId);
    }
    public class DeleteCourse : IDeleteCourse
    {
        private readonly ICourseRepository _courseRepository;
        public DeleteCourse(ICourseRepository courseRepository)
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
