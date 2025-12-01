using Schoole.Models;
using Schoole.Services.CourseService;
using Schoole.Services.GradeService;
using Schoole.Services.StudentsService;

namespace Schoole.MenuHelper
{
    public class GradeMenu
    {
        private readonly IAddGrade _addGrade;
        private readonly IGetAllStudents _getAllStudents;
        private readonly IGetAllCourses _getAllCourses;
        private readonly IShowReportCard _showReportCard;

        public GradeMenu(
            IAddGrade addGrade,
            IGetAllStudents getAllStudents,
            IGetAllCourses getAllCourses,
            IShowReportCard showReportCard)
        {
            _addGrade = addGrade;
            _getAllStudents = getAllStudents;
            _getAllCourses = getAllCourses;
            _showReportCard = showReportCard;

        }

        public void GradeMenuMain()
        {
            while (true)
            {
                Header("Grade Menu");
                Console.WriteLine("1. Add Grade");
                Console.WriteLine("2. Show student report card");
                Console.WriteLine("0. Go To Main Menu");
                LineUi();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        bool result = AddGrade();
                        if (result)
                        {
                            while (true)
                            {
                                LineUi();
                                Console.WriteLine("If you want to add a new grade, press 1.");
                                Console.WriteLine("To go back press 0");
                                int close = Convert.ToInt16(Console.ReadLine());
                                if (close == 1)
                                {
                                    AddGrade();
                                }
                                else if (close == 0)
                                {
                                    break;
                                }
                            }
                        }

                        break;
                    case "2":
                        ShowStudentReportCard();
                        break;
                    case "0":

                        return;
                    default:
                        ControlInput();
                        break;
                }
            }
        }

        public bool AddGrade()
        {
            Header("Adding Grade");
            List<Student> students = _getAllStudents.Execute();
            List<Course> courses = _getAllCourses.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (students.Count == 0 || courses.Count == 0)
            {
                Console.WriteLine("You cannot perform this operation unless you have students or courses.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return false;

            }
            else
            {
                Console.WriteLine($"Total Students: {students.Count}");
                foreach (Student student in students)
                {
                    LineUi();
                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");
                }

                LineUi();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Total courses: {courses.Count}");
                foreach (Course course in courses)
                {
                    LineUi();
                    Console.WriteLine($"Id: {course.ID}");
                    Console.WriteLine($"Name: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");
                }
                LineUi();
                Console.WriteLine("Student Id: ");
                var sId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Course Id: ");
                var cId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Score: ");
                var score = Convert.ToDouble(Console.ReadLine());
                var result = _addGrade.Execute(sId, cId, score);
                LineUi();

                if (result.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result.Message);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Message);
                    Console.ResetColor();
                }
                return true;
            }

        }

        public void ShowStudentReportCard()
        {
            Header("Show student report card");
            List<Student> students = _getAllStudents.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (students.Count == 0)
            {
                Console.WriteLine("No students have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Students: {students.Count}");
                foreach (Student student in students)
                {
                    LineUi();
                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");


                }
                LineUi();
                Console.Write("Student ID: ");
                var stId = int.Parse(Console.ReadLine());
                var result1 = _showReportCard.Execute(stId);
                LineUi();
                if (result1.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result1.Message);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result1.Message);
                    Console.ResetColor();
                }
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");

                Console.ReadKey();

            }

        }

        private void ControlInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid selection. Please try again.");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private void Header(string text)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{text}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
        }

        private void LineUi()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
        }
    }
}
