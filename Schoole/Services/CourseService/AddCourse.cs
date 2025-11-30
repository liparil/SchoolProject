using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IAddCourse
    {
        Output Execute(string title, int teacherId);
    }
    public class AddCourse : IAddCourse
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;

        public AddCourse(ITeacherRepository teacherRepository, ICourseRepository courseRepository)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
        }

        public Output Execute(string title, int teacherId)
        {
            var teacher = _teacherRepository.GetTeacherById(teacherId);
            var output = new Output();

            if (teacher == null)
            {
                output.Success = false;
                output.Message = "Teacher Dose Not Exist!";
                return output;
            }

            var newCourse = new Course
            {
                Title = title,
                TeacherId = teacherId
            };

            _courseRepository.Add(newCourse);
            teacher.Courses.Add(newCourse);
            output.Success = true;
            output.Message = $"Course {title} was added and {teacher.FullName} is its teacher.";
            return output;
        }
    }
}
