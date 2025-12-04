using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IAssignCourseToClassroom
    {
        Task<Output> Execute(int courseId, int classroomId);
    }
    public class AssignCourseToClassroom(IClassroomRepository classroomRepository, ICourseRepository courseRepository,ILogService logService) : IAssignCourseToClassroom
    {
        public async Task<Output> Execute(int courseId, int classroomId)
        {
            var course = courseRepository.GetCourseById(courseId);
            var classroom = classroomRepository.GetClassroomById(classroomId);
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
            await logService.LogCreate($"Assign Course To Classroom : {course.Title} - {classroom.Name}");
            return output;
        }
    }
}
