using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IGetClassroomById
    {
        Classroom Execute(int classroomId);
    }
    public class GetClassroomById(AppDbContext context) : IGetClassroomById
    {
        public Classroom Execute(int classroomId)
        {
            return context.Classrooms.Include(c => c.Students).FirstOrDefault(c => c.ID == classroomId);
        }
    }
}
