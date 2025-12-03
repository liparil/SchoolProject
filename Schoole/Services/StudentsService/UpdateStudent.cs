using Schoole.Interfaces;
using Schoole.Models;
using Schoole.Services.LogModelService;
using System.Threading.Tasks;

namespace Schoole.Services.StudentsService
{
    public interface IUpdateStudent
    {
        Task Execute(Student student);
    }
    public class UpdateStudent(IStudentRepository studentRepository, ILogService logService ) : IUpdateStudent
    {
        public async Task Execute(Student student)
        {
            studentRepository.Update(student);

            await logService.LogInfo($"Student updated: {student.FullName} - {student.NCode}");
        }
    }
}
