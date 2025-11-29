using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IGetStudentByIdService
    {
        Student Execute(int studentId);
    }

    public class GetStudentByIdService : IGetStudentByIdService
    {

        private readonly AppDbContext _appDbContext;


        public GetStudentByIdService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Student Execute(int studentId)
        {
            return _appDbContext.Students
                .Include(s => s.Grades)
                .ThenInclude(g => g.Course)
                .FirstOrDefault(s => s.ID == studentId);
        }
    }
}