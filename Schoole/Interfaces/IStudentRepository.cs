using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Update(Student student);
        void Delete(int studentId);
        Student GetStudentById(int studentId);
        List<Student> GetAllStudents();
        Student GetStudentByNcode(string nCode);
        void save();


    }
}
