using Microsoft.EntityFrameworkCore;
using Schoole.Data;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IGetClassroomByIdService
    {
        Classroom Execute(int classroomId);
    }
    public class GetClassroomByIdService : IGetClassroomByIdService
    {
        private readonly AppDbContext _context;
        public GetClassroomByIdService(AppDbContext context)
        {
            _context = context;
        }

        public Classroom Execute(int classroomId)
        {
            return _context.Classrooms.Include(c => c.Students).FirstOrDefault(c => c.ID == classroomId);
        }
    }
}
