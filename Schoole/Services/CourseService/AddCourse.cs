using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.CourseService
{
    public interface IAddCourse
    {
        Output Execute(string title, int teacherId);
    }
    public class AddCourse(ITeacherRepository teacherRepository, ICourseRepository courseRepository) : IAddCourse
    {

        public Output Execute(string title, int teacherId)
        {
            var teacher = teacherRepository.GetTeacherById(teacherId);
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

            courseRepository.Add(newCourse);
            teacher.Courses.Add(newCourse);
            output.Success = true;
            output.Message = $"Course {title} was added and {teacher.FullName} is its teacher.";
            return output;
        }
    }
}
