using Schoole.Interfaces;

namespace Schoole.Services.CourseService
{
    public interface IDeleteCourse
    {
        Task Execute(int courseId);
    }
    public class DeleteCourse(ICourseRepository courseRepository, ILogService logService) : IDeleteCourse
    {
        public async Task Execute(int courseId)
        {
            var course = courseRepository.GetCourseById(courseId);

            if (course != null && course.teacher != null)
            {
                course.teacher.Courses.Remove(course);
            }

            courseRepository.Delete(courseId);
            await logService.LogDelete($"Course deleted: {course.Title} - Teacher Name: {course.teacher}");
        }
    }
}
