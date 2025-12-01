using Schoole.Interfaces;

namespace Schoole.Services.CourseService
{
    public interface IDeleteCourse
    {
        void Execute(int courseId);
    }
    public class DeleteCourse(ICourseRepository courseRepository) : IDeleteCourse
    {
        public void Execute(int courseId)
        {
            var course = courseRepository.GetCourseById(courseId);

            if (course != null && course.teacher != null)
            {
                course.teacher.Courses.Remove(course);
            }

            courseRepository.Delete(courseId);
        }
    }
}
