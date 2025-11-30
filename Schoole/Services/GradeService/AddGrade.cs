using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IAddGrade
    {
        Output Execute(int studentId, int courseId, double score);
    }
    public class AddGrade : IAddGrade
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IGradeRepository _gradeRepository;
        public AddGrade(ICourseRepository courseRepository, IStudentRepository studentRepository, IGradeRepository gradeRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
        }

        public Output Execute(int studentId, int courseId, double score)
        {
            var student = _studentRepository.GetStudentById(studentId);
            var course = _courseRepository.GetCourseById(courseId);

            var test = new Output();

            if (student == null || course == null)
            {
                test.Success = false;
                test.Message = "Student Or Course Does Not Exist!";
                return test;
            }

            var newGrade = new Grade
            {
                StudentId = studentId,
                CourseId = courseId,
                Score = score
            };

            _gradeRepository.Add(newGrade);

            test.Success = true;
            test.Message = $"A score of {score} was recorded for student {student.FullName} in course {course.Title}.";
            return test;
        }
    }
}
