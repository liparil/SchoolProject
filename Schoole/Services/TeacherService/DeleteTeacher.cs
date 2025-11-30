using Schoole.Interfaces;

namespace Schoole.Services.TeacherService
{
    public interface IDeleteTeacher
    {
        void Execute(int studentId);
    }
    public class DeleteTeacher : IDeleteTeacher
    {
        private readonly ITeacherRepository _teacherRepository;
        public DeleteTeacher(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public void Execute(int teacherId)
        {
            _teacherRepository.Delete(teacherId);
        }
    }
}
