using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IGetTeacherById
    {
        Teacher Execute(int teacherId);
    }
    public class GetTeacherById(AppDbContext context) : IGetTeacherById
    {

        public Teacher Execute(int teacherId)
        {
            return context.Teachers.FirstOrDefault(t => t.ID == teacherId);
        }
    }
}
