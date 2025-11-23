using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public GradeService(IGradeRepository gradeRepository, IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _gradeRepository = gradeRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public OutputTest AddGrade(int studentId, int courseId, double score)
        {
            var student = _studentRepository.GetStudentById(studentId);
            var course = _courseRepository.GetCourseById(courseId);

            var test = new OutputTest();

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

        public double CalculateAverage(int studentId)
        {
            var grades = _gradeRepository.GetGradeByStudentId(studentId);
            if (grades == null || !grades.Any())
                return 0;

            return grades.Average(g => g.Score);
        }

        public void deleteGrade(int gradeId)
        {
            var grade = _gradeRepository.GetGradeById(gradeId);
            if(grade != null)
            {
                
                grade.Student?.Grades.Remove(grade);
                _gradeRepository.Delete(gradeId);
            }
        }

        public List<Grade> GetGradesByCourse(int courseId)
        {
           return _gradeRepository.GetGradeByCourseId(courseId);
        }

        public List<Grade> GetGradesByStudent(int studentId)
        {
            return _gradeRepository.GetGradeByStudentId(studentId);
        }

        public void UpdateGrade(Grade grade)
        {
            _gradeRepository.Update(grade);
        }
    }
}
