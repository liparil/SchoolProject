using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);

        void Update(Course course);

        void Delete(int courseId);

        Course GetCourseById(int courseId);

        List<Course> GetAll();

        List<Course> GetCourseByTeacherId(int teacherId);
    }
}
