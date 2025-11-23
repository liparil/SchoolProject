using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface IStudentService
    {
        (bool Success, string Message) AddStudent(string fullName, DateTime birthDate, string nCode);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        (bool Success, string Message) ShowReportCard(int studentId);
        (bool Success, string Message) ShowStudent(string nCode);
        List<Student> GetAllStudents();
       

    }
}
