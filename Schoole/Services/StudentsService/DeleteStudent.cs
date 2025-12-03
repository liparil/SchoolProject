using Schoole.Interfaces;
using Schoole.Repositories.Database;
using Schoole.Services.LogModelService;
using System.Threading.Tasks;

namespace Schoole.Services.StudentsService
{
    public interface IDeleteStudent
    {
        Task Execute(int studentId);
    }
    public class DeleteStudent(IStudentRepository studentRepository, ILogService logService) : IDeleteStudent
    {
        public async Task Execute(int studentId)
        {
            var student = studentRepository.GetStudentById(studentId);
            if (student == null)
            {
                //await logService.LogWarning($"Delete failed: Student with ID {studentId} was not found.");
                return;
            }

            studentRepository.Delete(studentId);
            await logService.LogInfo($"Student deleted: {student.FullName} - {student.NCode}");
        }
    }
}
