using Schoole.Interfaces;
using Schoole.Models;
using System.Text;

namespace Schoole.Services.StudentsService
{
    public interface IShowReportCard
    {
        Output Execute(int studentId);
    }
    internal class ShowReportCard : IShowReportCard
    {
        private readonly IStudentRepository _studentRepository;
        public ShowReportCard(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Output Execute(int studentId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            var output = new Output();

            if (student == null)
            {
                output.Success = false;
                output.Message = "Student not found!";
                return output;
            }



            if (student.Grades == null || !student.Grades.Any())
            {
                output.Success = false;
                output.Message = $"{student.FullName} has no grades yet.";
                return output;
            }

            var report = new StringBuilder();

            report.AppendLine($"Report Card for {student.FullName} ({student.NCode}):");
            report.AppendLine("----------------------------");

            foreach (var grade in student.Grades)
                report.AppendLine($"{grade.Course.Title}: {grade.Score}");

            output.Success = true;
            output.Message = report.ToString();
            return output;
        }
    }
}
