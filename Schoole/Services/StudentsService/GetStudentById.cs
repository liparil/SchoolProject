using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IGetStudentById
    {
        Student Execute(int studentId);
    }

    public class GetStudentById(AppDbContext appDbContext) : IGetStudentById
    {
        public Student Execute(int studentId)
        {
            return appDbContext.Students
                .Include(s => s.Grades)
                .ThenInclude(g => g.Course)
                .FirstOrDefault(s => s.ID == studentId);
        }
    }
}