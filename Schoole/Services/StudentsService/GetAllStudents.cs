using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IGetAllStudents
    {
        Task<List<Student>> Execute();
    }
    public class GetAllStudents(IStudentRepository studentRepository, ILogService logService) : IGetAllStudents
    {
        public async Task<List<Student>> Execute()
        {
            await logService.LogRead($"All Students fetched. Count: {studentRepository.GetAllStudents().Count}");
            return studentRepository.GetAllStudents();
            
        }
    }
}
