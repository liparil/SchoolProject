using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface IClassroomService
    {
        (bool Success, string Message) AddClassroom(string name);
        void UpdateClassroom(Classroom classroom);
        void DeleteClassroom(int classroomId);
        List<Classroom> GetAllClassrooms();
        (bool Success, string Message) AddStudentToClassroom(int studentId,int classroomId);
        (bool Success, string Message) AssignCourseToClassroom(int courseId, int classroomId);

    }
}
       