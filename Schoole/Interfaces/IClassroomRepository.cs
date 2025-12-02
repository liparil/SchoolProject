using Schoole.Models;

namespace Schoole.Interfaces
{
    public interface IClassroomRepository
    {
        void Add(Classroom classroom);

        void Update(Classroom classroom);

        void Delete(int classroomId);

        Classroom GetClassroomById(int classroomId);

        List<Classroom> GetAllClassrooms();

        Classroom GetClassroomByName (string name);
    }
}
