using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        public (bool Success, string Message) AddCourse(string title, int teacherId)
        {
            var teacher = _teacherRepository.GetTeacherById(teacherId);

            if (teacher == null)
            {
                return (false, "Teacher Dose Not Exist!");
            }

            var newCourse = new Course
            {
                Title = title,
                TeacherId = teacherId
            };

            _courseRepository.Add(newCourse);
            teacher.Courses.Add(newCourse);
            return (true, $"Course {title} was added and {teacher.FullName} is its teacher.");
        }

        public void DeleteCourse(int courseId)
        {
            var course = _courseRepository.GetCourseById(courseId);

            if (course != null && course.teacher != null)
            {
                course.teacher.Courses.Remove(course);
            }

            _courseRepository.Delete(courseId);
        }

        public List<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public void UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
        }
    }
}
