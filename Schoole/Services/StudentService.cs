using Microsoft.EntityFrameworkCore;
using Schoole.Interfaces;
using Schoole.Models;
using System.Text;

namespace Schoole.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public (bool Success, string Message) AddStudent( string fullName, DateTime birthDate, string nCode)
        {
            var existing = _studentRepository.GetStudentByNcode(nCode);

            if (existing != null)
            {
                return (false, "A student with this national code already exists!");
            }

            var student = new Student
            {
                //ID = _studentRepository.GetAllStudents().Count + 1,
                FullName = fullName,
                BirthDate = birthDate,
                NCode = nCode
            };

            _studentRepository.Add(student);

            return (true, "Student added successfully!");
        }

        public void DeleteStudent(int studentId)
        {
            _studentRepository.Delete(studentId);
        }

        public List<Student> GetAllStudents()
        {
           return _studentRepository.GetAllStudents();
        }

       

        public (bool Success, string Message) ShowReportCard(int studentId)
        {

            var student = _studentRepository.GetStudentById(studentId);
            

            if (student == null)
                return (false, "Student not found!");

            if (student.Grades == null || !student.Grades.Any())
                return (false, $"{student.FullName} has no grades yet.");

            var report = new StringBuilder();

            report.AppendLine($"Report Card for {student.FullName} ({student.NCode}):");
            report.AppendLine("----------------------------");

            foreach (var grade in student.Grades)
                report.AppendLine($"{grade.Course.Title}: {grade.Score}");
            
            return (true, report.ToString());
        }

        public (bool Success, string Message) ShowStudent(string nCode)
        {
            var existing = _studentRepository.GetStudentByNcode(nCode);

            if (existing == null)
            {
                return (false, "No student with this national code was found.");
            }
            else
            {
                return (true, $"Student successfully found.\nName: {existing.FullName}\nNational code: {existing.NCode}\nBirthDate: {existing.BirthDate.ToShortDateString()}");
            }

        }

        public void UpdateStudent(Student student)
        {
            _studentRepository.Update(student);
        }
    }
}
