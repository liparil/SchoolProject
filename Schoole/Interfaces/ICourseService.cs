using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface ICourseService
    {
        (bool Success, string Message) AddCourse(string title, int teacherId);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
        List<Course> GetAllCourses();
    }
}
