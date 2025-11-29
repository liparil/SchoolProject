using Schoole.Interfaces;

namespace Schoole.Services.TeacherService
{
    public interface IDeleteTeacherService
    {
        void Execute(int studentId);
    }
    public class DeleteTeacherService : IDeleteTeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public DeleteTeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public void Execute(int teacherId)
        {
            _teacherRepository.Delete(teacherId);
        }
    }
}
