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
            IShowStudent showStudent)
        {
            _addStudent = addStudent;
            _addStudent = addStudent;
            _getAllStudents = getAllStudents;
            _getStudentById = getStudentById;
            _deleteStudent = deleteStudent;
            _updateStudent = updateStudent;
            _showStudent = showStudent;

        }


        public async Task StudentMenuMain()
        {
            while (true)
            {
                Console.Clear();
                Header("Students Menu");
                Console.WriteLine("1. Add Students");
                Console.WriteLine("2. Update Students");
                Console.WriteLine("3. Delete Students");
                Console.WriteLine("4. Show All Students");
                Console.WriteLine("5. Search By National Code");
                Console.WriteLine("0. Go To Main Menu");
                LineUi();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": await AddingStudent(); break;
                    case "2": await UpdateStudents(); break;
                    case "3": await DeleteStudents(); break;
                    case "4": ShowAllStudents(); break;
                    case "5": SearchByNationalCode(); break;
                    case "0": return;
                    default: ControlInput(); break;
                }
            }
        }

        public async Task AddingStudent()
        {
            Header("Adding Student");
            Console.WriteLine("Student Name: ");
            var sName = Console.ReadLine();

            string nCode;
            while (true)
            {
                Console.WriteLine("Enter national code: ");
                nCode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nCode))
                {
                    TextColor("National code cannot be empty!\n", "Red");
                    continue;
                }

                if (!nCode.All(char.IsDigit))
                {
                    TextColor("National code must contain only numbers!\n", "Red");
                    continue;
                }

                if (nCode.Length != 10)
                {
                    TextColor("National code should be exactly 10 numbers!\n", "Yellow");
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
                TextColor("The date format is incorrect. Please enter it again.\n", "Red");
            }

            await LoadingSpinner();
            var result = await _addStudent.Execute(sName, birth, nCode);

            LineUi();

            if (result.Success)
            {
                TextColor($"{result.Message}\n", "Green");
            }
            else
            {
                TextColor($"{result.Message}\n", "Red");
            }
            

            Console.WriteLine("Press any key to go back.");
            Console.ReadKey(true);
        }

        public void ShowAllStudents()
        {
            Header("Showing Student");
            List<Student> students = _getAllStudents.Execute();
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
                    LineUi();
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");

                }
            }

            LineUi();
            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
        }

        public async Task DeleteStudents()
        {
            Header("Delete Student");
            List<Student> students = _getAllStudents.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (students.Count == 0)
            {
                
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
                LineUi();
                Console.WriteLine($"Id: {student.ID}");
                Console.WriteLine($"Name: {student.FullName}");
                Console.WriteLine($"National code: {student.NCode}");
                Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");
            }
            LineUi();

            int stdDeleteId;
            bool isValidInput = false;

            do
            {
                Console.Write("Enter the student ID you want to delete:");
                TextColor(" (Or press 0 to cancel)\n", "Yellow");
                string input = Console.ReadLine();

                if (input == "0") return;

                isValidInput = int.TryParse(input, out stdDeleteId) && stdDeleteId > 0;

                if (!isValidInput)
                {
                    TextColor("Invalid ID. Please enter a positive number.\n", "Red");
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
                LineUi();
                Console.Write($"Are you sure you want to delete ");
                TextColor($"{studentToDelete.FullName}", "Red");
                Console.WriteLine("?");
                Console.Write("1. ");
                TextColor("Yes", "Red");
                Console.Write("    2. ");
                TextColor("No\n", "Green");
                LineUi();
                Console.Write("Select an option (1/2): ");

                if (int.TryParse(Console.ReadLine(), out int makeSure))
                {
                    if (makeSure == 1)
                    {
                        await LoadingSpinner();
                        await _deleteStudent.Execute(stdDeleteId);
                        LineUi();
                        TextColor("Student deleted successfully!\n", "Green");
                        break;
                    }
                    else if (makeSure == 2)
                    {
                        TextColor("Deletion cancelled.\n", "Yellow");
                        break;
                    }
                }
                TextColor("Invalid selection. Please enter 1 or 2.\n", "Red");
            }

            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
        }

        public async Task UpdateStudents()
        {
            Header("Update Student");
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
                LineUi();
                foreach (Student student in students)
                {

                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Date of birth: {student.BirthDate.ToShortDateString()}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    LineUi();

                }
                Console.WriteLine("Enter the student ID you want to edit: ");
                var stdId = Convert.ToInt32(Console.ReadLine());
                var studentResult = _getStudentById.Execute(stdId);
                if (studentResult == null)
                {
                    LineUi();
                    TextColor("No student with this ID was found.\n", "Red");
                    LineUi();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();

                }
                else
                {
                    Header("Edit Student");
                    TextColor($"The student You Want To Edit:\n", "Green");
                    Console.WriteLine($"Name: {studentResult.FullName}\nNational code: {studentResult.NCode}\nDate of birth: {studentResult.BirthDate} ");
                    LineUi();
                    Console.WriteLine("What do you want to edit?");
                    Console.WriteLine("     1. Edit Name");
                    Console.WriteLine("     2. Edit National code");
                    Console.WriteLine("     3. Edit Date of birth");
                    Console.WriteLine("     4. Edit All Information");
                    Console.WriteLine("     0. Cancel");
                    LineUi();
                    Console.Write("Your choice: ");
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Header("Edit Name");
                            Console.WriteLine("Enter New Name: ");
                            var onlyName = Console.ReadLine();
                            var updatedStudent = new Student
                            {
                                ID = studentResult.ID,
                                FullName = onlyName,
                                NCode = studentResult.NCode,
                                BirthDate = studentResult.BirthDate
                            };
                            await LoadingSpinner();
                            await _updateStudent.Execute(updatedStudent);
                            LineUi();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The student successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "2":
                            Header("Edit National code");
                            string onlyNCode;
                            while (true)
                            {
                                Console.WriteLine("Enter New National code: ");
                                onlyNCode = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(onlyNCode))
                                {
                                    TextColor("National code cannot be empty!\n", "Red");
                                    continue;
                                }

                                if (!onlyNCode.All(char.IsDigit))
                                {
                                    TextColor("National code must contain only numbers!\n", "Red");
                                    continue;
                                }

                                if (onlyNCode.Length != 10)
                                {
                                    TextColor("National code should be exactly 10 numbers!\n", "Yellow");
                                    continue;
                                }

                                var updatedNCodeStudent = new Student
                                {
                                    ID = studentResult.ID,
                                    FullName = studentResult.FullName,
                                    NCode = onlyNCode,
                                    BirthDate = studentResult.BirthDate
                                };
                                await LoadingSpinner();
                                await _updateStudent.Execute(updatedNCodeStudent);
                                LineUi();
                                TextColor("The student successfully changed\n", "Green");
                                Console.WriteLine("Press Any Key To Go Back.");
                                Console.ReadKey();
                                break;
                            }
                            break;

                        case "3":
                            Header("Edit Date of birth");
                            Console.WriteLine("Enter New Date of birth: ");
                            DateTime onlyBDate;
                            while (!DateTime.TryParse(Console.ReadLine(), out onlyBDate))
                            {
                                TextColor("Invalid date format. Try again (yyyy-MM-dd): \n", "Yellow");
                            }
                            var updatedBDateStudent = new Student
                            {
                                ID = studentResult.ID,
                                FullName = studentResult.FullName,
                                NCode = studentResult.NCode,
                                BirthDate = onlyBDate
                            };
                            await LoadingSpinner();
                            await _updateStudent.Execute(updatedBDateStudent);
                            LineUi();
                            TextColor("The student successfully changed\n", "Green");
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "4":
                            Header("Edit All Information");
                            Console.WriteLine("Enter New Name: ");
                            var nStdName = Console.ReadLine();
                            string nStdNCode;
                            while (true)
                            {
                                Console.WriteLine("Enter national code: ");
                                nStdNCode = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nStdNCode))
                                {
                                    TextColor("National code cannot be empty!\n", "Red");
                                    continue;
                                }

                                if (!nStdNCode.All(char.IsDigit))
                                {
                                    TextColor("National code must contain only numbers!\n", "Red");
                                    continue;
                                }

                                if (nStdNCode.Length != 10)
                                {
                                    TextColor("National code should be exactly 10 numbers!\n", "Yellow");
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
                                await LoadingSpinner();
                                await _updateStudent.Execute(updatedAllStudent);
                                LineUi();
                                TextColor("The student successfully changed\n", "Green");
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
            Header("Search By National Code");
            List<Student> students = _getAllStudents.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            if (students.Count == 0)
            {
                Console.WriteLine("No students have been added.");
                LineUi();

            }
            else
            {
                Console.WriteLine("Enter The National Code:");
                string Code = Console.ReadLine();
                var std = _showStudent.Execute(Code);
                LineUi();
                if (std.Success)
                {
                    TextColor($"{std.Message}\n", "Green");
                }
                else
                {
                    TextColor($"{std.Message}\n", "Red");
                }
                LineUi();
            }
            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
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

            Console.Write("\rLoading... Done!   \n");
        }
    }
}