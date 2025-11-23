using Schoole.Interfaces;
using Schoole.Models;
using Schoole.Repositories;
using Schoole.Repositories.Database;
using Schoole.Services;

namespace Schoole.MenuHelper
{
    public class StudentMenu
    {
        private readonly MenuHelper _menuHelper;

        public StudentMenu(MenuHelper menuHelper)
        {
            _menuHelper = menuHelper;
        }

        public void Student(DbStudentRepository studentRepo, StudentService studentService)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("*** ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Students Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" ***");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("1. Add Students");
                Console.WriteLine("2. Update Students");
                Console.WriteLine("3. Delete Students");
                Console.WriteLine("4. Show All Students");
                Console.WriteLine("5. Search By National Code");
                Console.WriteLine("0. Go To Main Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddingStudent(studentRepo, studentService);
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("If you want to add a new student, press 1.");
                            Console.WriteLine("To go back press 0");
                            int close = Convert.ToInt16(Console.ReadLine());
                            if (close == 1)
                            {
                                AddingStudent(studentRepo, studentService);
                            }
                            else if (close == 0)
                            {
                                break;
                            }
                        }
                        break;

                    case "2":
                        UpdateStudents(studentRepo, studentService);
                        break;
                    case "3":
                        DeleteStudents(studentRepo, studentService);
                        break;
                    case "4":
                        ShowAllStudents(studentRepo, studentService);
                        break;
                    case "5":
                        SearchByNationalCode(studentRepo, studentService);
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

        public void AddingStudent(DbStudentRepository studentRepo, StudentService studentService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Adding Student");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.WriteLine("Student Name: ");
            var sName = Console.ReadLine();

            string nCode;

            while (true)
            {
                Console.WriteLine("Enter national code: ");
                nCode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nCode))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("National code cannot be empty!");
                    Console.ResetColor();
                    continue;
                }

                if (!nCode.All(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("National code must contain only numbers!");
                    Console.ResetColor();
                    continue;
                }

                if (nCode.Length != 10)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("National code should be exactly 10 numbers!");
                    Console.ResetColor();
                    continue;
                }

                break;
            }



            DateTime birth;
            while (true)
            {
                Console.WriteLine("Date of birth (yyyy-MM-dd): ");
                string dBirth = Console.ReadLine();
                if (DateTime.TryParse(dBirth, out birth))
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The date format is incorrect. Please enter it again.");
                Console.ResetColor();
            }
            var result = studentService.AddStudent(sName, birth, nCode);
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

        }

        public void UpdateStudents(DbStudentRepository studentRepo, StudentService studentService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Update Student");
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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                foreach (Student student in students)
                {

                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();

                }
                Console.WriteLine("Enter the student ID you want to edit: ");
                var stdId = Convert.ToInt32(Console.ReadLine());
                var studentResult = studentRepo.GetStudentById(stdId);
                if (studentResult == null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No student with this ID was found.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();

                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("*** ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Edit Student");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" ***");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The student You Want To Edit:");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {studentResult.FullName}\nNational code: {studentResult.NCode}\nDate of birth: {studentResult.BirthDate} ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine("What do you want to edit?");
                    Console.WriteLine("     1. Edit Name");
                    Console.WriteLine("     2. Edit National code");
                    Console.WriteLine("     3. Edit Date of birth");
                    Console.WriteLine("     4. Edit All Information");
                    Console.WriteLine("     0. Cancel");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.Write("Your choice: ");
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("*** ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Edit Name");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" ***");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("Enter New Name: ");
                            var onlyName = Console.ReadLine();
                            var updatedStudent = new Student
                            {
                                ID = studentResult.ID,
                                FullName = onlyName,
                                NCode = studentResult.NCode,
                                BirthDate = studentResult.BirthDate
                            };
                            studentService.UpdateStudent(updatedStudent);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The student successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "2":
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("*** ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Edit National code");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" ***");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            
                            string onlyNCode;

                            
                            while (true)
                            {
                                Console.WriteLine("Enter New National code: ");
                                onlyNCode = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(onlyNCode))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("National code cannot be empty!");
                                    Console.ResetColor();
                                    continue;
                                }

                                if (!onlyNCode.All(char.IsDigit))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("National code must contain only numbers!");
                                    Console.ResetColor();
                                    continue;
                                }

                                if (onlyNCode.Length != 10)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("National code should be exactly 10 numbers!");
                                    Console.ResetColor();
                                    continue;
                                }

                                var updatedNCodeStudent = new Student
                                {
                                    ID = studentResult.ID,
                                    FullName = studentResult.FullName,
                                    NCode = onlyNCode,
                                    BirthDate = studentResult.BirthDate
                                };
                                studentService.UpdateStudent(updatedNCodeStudent);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------------------");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("The student successfully changed");
                                Console.ResetColor();
                                Console.WriteLine("Press Any Key To Go Back.");
                                Console.ReadKey();
                                break;
                            }

                            
                            break;

                        case "3":
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("*** ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Edit Date of birth");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" ***");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("Enter New Date of birth: ");
                            DateTime onlyBDate;
                            while (!DateTime.TryParse(Console.ReadLine(), out onlyBDate))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Invalid date format. Try again (yyyy-MM-dd): ");
                                Console.ResetColor();
                            }
                            var updatedBDateStudent = new Student
                            {
                                ID = studentResult.ID,
                                FullName = studentResult.FullName,
                                NCode = studentResult.NCode,
                                BirthDate = onlyBDate
                            };
                            studentService.UpdateStudent(updatedBDateStudent);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The student successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "4":
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("*** ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Edit All Information");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" ***");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("Enter New Name: ");
                            var nStdName = Console.ReadLine();
                            string nStdNCode;

                            while (true)
                            {
                                Console.WriteLine("Enter national code: ");
                                nStdNCode = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nStdNCode))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("National code cannot be empty!");
                                    Console.ResetColor();
                                    continue;
                                }

                                if (!nStdNCode.All(char.IsDigit))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("National code must contain only numbers!");
                                    Console.ResetColor();
                                    continue;
                                }

                                if (nStdNCode.Length != 10)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("National code should be exactly 10 numbers!");
                                    Console.ResetColor();
                                    continue;
                                }

                                Console.WriteLine("Enter New Birth Date: ");

                                DateTime nStdBDate;
                                while (!DateTime.TryParse(Console.ReadLine(), out nStdBDate))
                                {
                                    Console.WriteLine("Invalid date format. Try again (yyyy-MM-dd): ");
                                }
                                var updatedAllStudent = new Student
                                {
                                    ID = studentResult.ID,
                                    FullName = nStdName,
                                    NCode = nStdNCode,
                                    BirthDate = nStdBDate
                                };
                                studentService.UpdateStudent(updatedAllStudent);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------------------");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("The student successfully changed");
                                Console.ResetColor();
                                Console.WriteLine("Press Any Key To Go Back.");
                                Console.ReadKey();
                                break;
                            }
                            break;

                    }

                    
                }

            }



        }

        public void DeleteStudents(DbStudentRepository studentRepo, StudentService studentService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Delete Student");
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
                    Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Enter the student ID you want to delete: ");
                var stdDeleteId = Convert.ToInt32(Console.ReadLine());
                var studentDeleteResult = studentRepo.GetStudentById(stdDeleteId);
                var makeSure = 0;
                if (studentDeleteResult == null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No student with this ID was found.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }
                else
                {
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("----------------------------");
                        Console.ResetColor();
                        Console.Write($"Are you sure you want to delete ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{studentDeleteResult.FullName}");
                        Console.ResetColor();
                        Console.WriteLine("?");
                        Console.Write("1. ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Yes");
                        Console.ResetColor();
                        Console.Write("   2. ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("No");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("----------------------------");
                        Console.ResetColor();
                        Console.Write("Select an option (");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("1");
                        Console.ResetColor();
                        Console.Write("/");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("2");
                        Console.ResetColor();
                        Console.Write("): ");




                        makeSure = Convert.ToInt32(Console.ReadLine());
                        if (makeSure == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            studentService.DeleteStudent(stdDeleteId);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The student successfully deleted.");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            break;
                        }

                    }
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }



            }
        }

        public void ShowAllStudents(DbStudentRepository studentRepo, StudentService studentService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Showing Student");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            List<Student> students = studentRepo.GetAllStudents();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (students.Count == 0)
            {
                Console.WriteLine("No students have been added.");
            }
            else
            {
                Console.WriteLine($"Total Students: {students.Count}");
                foreach (Student student in students)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");

                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
        }

        public void SearchByNationalCode(DbStudentRepository studentRepo, StudentService studentService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Search By National Code");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();

            List<Student> students = studentRepo.GetAllStudents();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            if (students.Count == 0)
            {
                Console.WriteLine("No students have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

            }
            else
            {
                Console.WriteLine("Enter The National Code:");
                string Code = Console.ReadLine();
                var std = studentService.ShowStudent(Code);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                if (std.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(std.Message);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(std.Message);
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
            }


            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
        }
    }
}
