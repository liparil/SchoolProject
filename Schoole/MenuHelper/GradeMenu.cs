using Schoole.Models;
using Schoole.Repositories;
using Schoole.Repositories.Database;
using Schoole.Services;

namespace Schoole.MenuHelper
{
    public class GradeMenu
    {
        private readonly MenuHelper _menuHelper;

        public GradeMenu(MenuHelper menuHelper)
        {
            _menuHelper = menuHelper;
        }

        public void Grade(DbGradeRepository gradeRepo, GradeService gradeService, DbStudentRepository studentRepo, StudentService studentService, DbCourseRepository courseRepo, CourseService courseService)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("*** ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Grade Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" ***");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("1. Add Grade");
                Console.WriteLine("2. Show student report card");
                Console.WriteLine("0. Go To Main Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        bool result = AddGrade(gradeRepo, gradeService, studentRepo, studentService, courseRepo, courseService);
                        if (result)
                        {
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------------------");
                                Console.ResetColor();
                                Console.WriteLine("If you want to add a new grade, press 1.");
                                Console.WriteLine("To go back press 0");
                                int close = Convert.ToInt16(Console.ReadLine());
                                if (close == 1)
                                {
                                    AddGrade(gradeRepo, gradeService, studentRepo, studentService, courseRepo, courseService);
                                }
                                else if (close == 0)
                                {
                                    break;
                                }
                            }
                        }
                        
                        break;
                    case "2":
                        ShowStudentReportCard(gradeRepo, gradeService, studentRepo, studentService, courseRepo, courseService);
                        break;
                    case "0":
                        _menuHelper.MainMenu();
                        return;
                    default:
                        _menuHelper.ControlInput();
                        break;
                }
            }
        }

        public bool AddGrade(DbGradeRepository gradeRepo, GradeService gradeService, DbStudentRepository studentRepo, StudentService studentService, DbCourseRepository courseRepo, CourseService courseService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Adding Grade");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Student> students = studentRepo.GetAllStudents();
            List<Course> courses = courseRepo.GetAll();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (students.Count == 0 || courses.Count == 0)
            {
                Console.WriteLine("You cannot perform this operation unless you have students or courses.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return false;
                
            }
            else
            {
                Console.WriteLine($"Total Students: {students.Count}");
                foreach (Student student in students)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Total courses: {courses.Count}");
                foreach (Course course in courses)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {course.ID}");
                    Console.WriteLine($"Name: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Student Id: ");
                var sId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Course Id: ");
                var cId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Score: ");
                var score = Convert.ToDouble(Console.ReadLine());
                var result = gradeService.AddGrade(sId, cId, score);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

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

        public void ShowStudentReportCard(DbGradeRepository gradeRepo, GradeService gradeService, DbStudentRepository studentRepo, StudentService studentService, DbCourseRepository courseRepo, CourseService courseService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Show student report card");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();

            List<Student> students = studentRepo.GetAllStudents();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (students.Count == 0)
            {
                Console.WriteLine("No students have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Students: {students.Count}");
                foreach (Student student in students)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");


                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.Write("Student ID: ");
                var stId = int.Parse(Console.ReadLine());
                var result1 = studentService.ShowReportCard(stId);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");

                Console.ReadKey();

            }

        }
    }
}
