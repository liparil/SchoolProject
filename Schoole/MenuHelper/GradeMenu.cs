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

        public async Task GradeMenuMain()
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

                        bool result =await AddGrade();
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
                                    await AddGrade();
                                }
                                else if (close == 0)
                                {
                                    break;
                                }
                            }
                        }

                        break;
                    case "2":
                        await ShowStudentReportCard();
                        break;
                    case "0":

                        return;
                    default:
                        ControlInput();
                        break;
                }
            }
        }

        public async Task<bool> AddGrade()
        {
            Header("Adding Grade");
            List<Student> students = await _getAllStudents.Execute();
            List<Course> courses = await _getAllCourses.Execute();
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
                TextColor($"Total courses: {courses.Count}\n", "Yellow");
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
                await LoadingSpinner();
                var result = await _addGrade.Execute(sId, cId, score);
                LineUi();

                if (result.Success)
                {
                    TextColor($"{result.Message}\n", "Green");
                    
                }
                else
                {
                    TextColor($"{result.Message}\n", "Red");
                }
                return true;
            }

        }

        public async Task ShowStudentReportCard()
        {
            Header("Show student report card");
            List<Student> students = await _getAllStudents.Execute();
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
                    LineUi();
                    Console.Clear();
                    TextColor($"{result1.Message}\n", "Green");
                }
                else
                {
                    TextColor($"{result1.Message}\n", "Red");
                }
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");

                Console.ReadKey();

            }

        }

        private void ControlInput()
        {
            TextColor("Invalid selection. Please try again.\n", "Red");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void Header(string text)
        {

            Console.Clear();
            TextColor("*** ", "Blue");
            TextColor($"{text}", "Cyan");
            TextColor(" ***\n", "Blue");
            LineUi();
        }

        private void LineUi()
        {
            TextColor("----------------------------\n", "Blue");
        }

        private void TextColor(string text, string color)
        {
            if (Enum.TryParse(typeof(ConsoleColor), color, true, out var parsedColor))
            {
                Console.ForegroundColor = (ConsoleColor)parsedColor;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(text);
            Console.ResetColor();
        }

        static async Task LoadingSpinner(int durationMs = 3000)
        {
            char[] frames = { '|', '/', '-', '\\' };
            int index = 0;
            int interval = 100;

            DateTime end = DateTime.Now.AddMilliseconds(durationMs);

            while (DateTime.Now < end)
            {
                Console.Write($"\rLoading... {frames[index]}");
                index = (index + 1) % frames.Length;
                await Task.Delay(interval);
            }

            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");

        }
    }

}
