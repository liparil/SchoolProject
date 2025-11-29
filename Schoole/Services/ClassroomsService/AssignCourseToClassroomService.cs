using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IAssignCourseToClassroomService
    {
        Output Execute(int courseId, int classroomId);
    }
    public class AssignCourseToClassroomService : IAssignCourseToClassroomService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IClassroomRepository _classroomRepository;
        public AssignCourseToClassroomService(IClassroomRepository classroomRepository, ICourseRepository courseRepository)
        {
            _classroomRepository = classroomRepository;
            _courseRepository = courseRepository;
        }

        public Output Execute(int courseId, int classroomId)
        {
            var course = _courseRepository.GetCourseById(courseId);
            var classroom = _classroomRepository.GetClassroomById(classroomId);
            var output = new Output();
            if (course == null)
            {
                output.Success = false;
                output.Message = "Course Does Not Exist";
                return output;
            }
            if (classroom == null)
            {
                output.Success = false;
                output.Message = "Classroom Does Not Exist";
                return output;
            }
            if (!classroom.Courses.Contains(course))
            {
                classroom.Courses.Add(course);
            }
            output.Success = true;
            output.Message = $"Course: {course.Title} Added to: {classroom.Name}";
            return output;
        }
    }
}
