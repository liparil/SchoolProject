using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.GradeService
{
    public interface IAddGrade
    {
        Output Execute(int studentId, int courseId, double score);
    }
    public class AddGrade(ICourseRepository courseRepository, IStudentRepository studentRepository, IGradeRepository gradeRepository) : IAddGrade
    {

        public Output Execute(int studentId, int courseId, double score)
        {
            var student = studentRepository.GetStudentById(studentId);
            var course = courseRepository.GetCourseById(courseId);

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

            gradeRepository.Add(newGrade);

            test.Success = true;
            test.Message = $"A score of {score} was recorded for student {student.FullName} in course {course.Title}.";
            return test;
        }
    }
}
