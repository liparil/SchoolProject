using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.ClassroomsService
{
    public interface IAddStudentToClassroom
    {
        Output Execute(int studentId, int classroomId);
    }
    public class AddStudentToClassroom : IAddStudentToClassroom
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IStudentRepository _studentRepository;
        public AddStudentToClassroom(IClassroomRepository classroomRepository, IStudentRepository studentRepository)
        {
            _classroomRepository = classroomRepository;
            _studentRepository = studentRepository;
        }

        public Output Execute(int studentId, int classroomId)
        {
            var classroom = _classroomRepository.GetClassroomById(classroomId);
            var student = _studentRepository.GetStudentById(studentId);
            var output = new Output();

            if (student == null)
            {
                output.Success = false;
                output.Message = "Student Does Not Exist";
                return output;
            }


            if (classroom == null)
            {
                output.Success = false;
                output.Message = "Classroom Does Not Exist";
                return output;
            }



            if (student.ClassroomId == classroomId)
            {
                output.Success = false;
                output.Message = "Student already in this classroom";
                return output;
            }

            if (student.ClassroomId != null)
            {
                student.ClassroomId = classroomId;
                _studentRepository.Update(student);
                _studentRepository.save();
                output.Success = true;
                output.Message = $"Student: {student.FullName} Added to: {classroom.Name}";
                return output;
            }
            else
            {
                output.Success = false;
                output.Message = "Student added in classroom";
                return output;
            }

        }
    }
}
