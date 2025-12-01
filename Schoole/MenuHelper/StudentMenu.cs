using Schoole.Models;
using Schoole.Services.StudentsService;

namespace Schoole.MenuHelper
{
    public class StudentMenu
    {
        private readonly IAddStudent _addStudent;
        private readonly IDeleteStudent _deleteStudent;
        private readonly IGetStudentById _getStudentById;
        private readonly IGetAllStudents _getAllStudents;
        private readonly IUpdateStudent _updateStudent;
        private readonly IShowStudent _showStudent;


        public StudentMenu(
        IAddStudent addStudent,
        IGetAllStudents getAllStudents,
        IGetStudentById getStudentById,
        IDeleteStudent deleteStudent,
        IUpdateStudent updateStudent,
        IShowStudent showStudent

            )
        {
            _addStudent = addStudent;
            _addStudent = addStudent;
            _getAllStudents = getAllStudents;
            _getStudentById = getStudentById;
            _deleteStudent = deleteStudent;
            _updateStudent = updateStudent;
            _showStudent = showStudent;

        }


        public void StudentMenuMain()
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
                        AddingStudent();
                        break;
                    case "2":
                        UpdateStudents();
                        break;
                    case "3":
                        DeleteStudents();
                        break;
                    case "4":
                        ShowAllStudents();
                        break;
                    case "5":
                        SearchByNationalCode();
                        break;
                    case "0":
                        return;
                    default:
                        ControlInput();
                        break;
                }
            }
        }

        public void AddingStudent()
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

            var result = _addStudent.Execute(sName, birth, nCode);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();

            if (result.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Message);
            }
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("If you want to add a new student, press 1.");
                Console.WriteLine("To go back press 0");

                if (int.TryParse(Console.ReadLine(), out int close))
                {
                    if (close == 1)
                    {
                        AddingStudent();
                        return;
                    }
                    else if (close == 0)
                    {
                        return;
                    }
                }
                ControlInput();
            }
        }

        public void ShowAllStudents()
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
            List<Student> students = _getAllStudents.Execute();
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

        public void DeleteStudents()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Delete Student");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Student> students = _getAllStudents.Execute();

            if (students.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No students have been added.");
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return;
            }

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

            int stdDeleteId;
            bool isValidInput = false;

            do
            {
                Console.WriteLine("Enter the student ID you want to delete:");
                Console.WriteLine("Or enter 0 to cancel");
                string input = Console.ReadLine();

                if (input == "0") return;

                isValidInput = int.TryParse(input, out stdDeleteId) && stdDeleteId > 0;

                if (!isValidInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID. Please enter a positive number.");
                    Console.ResetColor();
                }

            } while (!isValidInput);

            var studentToDelete = students.FirstOrDefault(s => s.ID == stdDeleteId);


            if (studentToDelete == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("----------------------------");
                Console.WriteLine("No student with this ID was found.");
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.Write($"Are you sure you want to delete ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{studentToDelete.FullName}");
                Console.ResetColor();
                Console.WriteLine("?");
                Console.Write("1. ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Yes");
                Console.ResetColor();
                Console.Write("    2. ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No");
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.Write("Select an option (1/2): ");

                if (int.TryParse(Console.ReadLine(), out int makeSure))
                {
                    if (makeSure == 1)
                    {

                        _deleteStudent.Execute(stdDeleteId);

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("----------------------------");
                        Console.ResetColor();
                        Console.ResetColor();
                        break;
                    }
                    else if (makeSure == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Deletion cancelled.");
                        Console.ResetColor();
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid selection. Please enter 1 or 2.");
                Console.ResetColor();
            }

            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
        }

        public void UpdateStudents()
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
            List<Student> students = _getAllStudents.Execute();
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
                var studentResult = _getStudentById.Execute(stdId);
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
                            _updateStudent.Execute(updatedStudent);
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
                                _updateStudent.Execute(updatedNCodeStudent);
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
                            _updateStudent.Execute(updatedBDateStudent);
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
                                _updateStudent.Execute(updatedAllStudent);
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

        public void SearchByNationalCode()
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
            List<Student> students = _getAllStudents.Execute();
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
                var std = _showStudent.Execute(Code);
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

        private void ControlInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid selection. Please try again.");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}