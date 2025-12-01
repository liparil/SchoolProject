using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IUpdateStudent
    {
        void Execute(Student student);
    }
    public class UpdateStudent(IStudentRepository studentRepository) : IUpdateStudent
    {
        public void Execute(Student student)
        {
            studentRepository.Update(student);
        }
    }
}
