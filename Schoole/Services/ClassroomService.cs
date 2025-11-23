using Microsoft.EntityFrameworkCore;
using Schoole.Interfaces;
using Schoole.Models;
using Schoole.Repositories.Database;

namespace Schoole.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        public ClassroomService(
            IClassroomRepository classroomRepository, IStudentRepository studentRepository, ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _classroomRepository = classroomRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }
        public (bool Success, string Message) AddClassroom(string name)
        {
            var newClassroom = new Classroom
            {
                Name = name
            };
            _classroomRepository.Add(newClassroom);
            return (true, $"{name} Added Sucssesfuly.");
        }
    
        public (bool Success, string Message) AddStudentToClassroom(int studentId, int classroomId)
        {
            var classroom = _classroomRepository.GetClassroomById(classroomId);
            var student = _studentRepository.GetStudentById(studentId);

            if (student == null)
                return (false, "Student Does Not Exist");

            if (classroom == null)
                return (false, "Classroom Does Not Exist");

            if (student.ClassroomId == classroomId)
                return (false, "Student already in this classroom");
            if(student.ClassroomId != null)
            {
                student.ClassroomId = classroomId;
                _studentRepository.Update(student);
                _studentRepository.save();

                return (true, $"Student: {student.FullName} Added to: {classroom.Name}");
            }
            else
            {
                return (false, "Student added in classroom");
            }

            
        }

        public (bool Success, string Message) AssignCourseToClassroom(int courseId, int classroomId)
        {
            var course = _courseRepository.GetCourseById(courseId);
            var classroom = _classroomRepository.GetClassroomById(classroomId);
            if (course == null)
            {
                return (false, "Course Does Not Exist");
            }
            if(classroom == null)
            {
                return (false, "Classroom Does Not Exist");
            }
            if (!classroom.Courses.Contains(course))
            {
                classroom.Courses.Add(course);
            }
            return (true, $"Course: {course.Title} Added to: {classroom.Name}");
        }

        public void DeleteClassroom(int classroomId)
        {
            _classroomRepository.Delete(classroomId);
            
        }

        public List<Classroom> GetAllClassrooms()
        {
            return _classroomRepository.GetAllClassrooms();
        }

        public void UpdateClassroom(Classroom classroom)
        {
            _classroomRepository.Update(classroom);
        }
    }
}
