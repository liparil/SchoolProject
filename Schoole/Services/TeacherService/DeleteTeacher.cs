using Schoole.Interfaces;

namespace Schoole.Services.TeacherService
{
    public interface IDeleteTeacher
    {
        void Execute(int studentId);
    }
    public class DeleteTeacher(ITeacherRepository teacherRepository) : IDeleteTeacher
    {
        public void Execute(int teacherId)
        {
            teacherRepository.Delete(teacherId);
        }
    }
}
