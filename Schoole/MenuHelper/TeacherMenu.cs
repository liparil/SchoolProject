using Schoole.Models;
using Schoole.Services.TeacherService;

namespace Schoole.MenuHelper
{
    public class TeacherMenu
    {
        private readonly IAddTeacher _addTeacher;
        private readonly IDeleteTeacher _deleteTeacher;
        private readonly IGetTeacherById _getTeacherById;
        private readonly IGetAllTeachers _getAllTeachers;
        private readonly IUpdateTeacher _UpdateTeacher;
        private readonly IShowTeacher _showTeacher;

        public TeacherMenu(
            IAddTeacher addTeacher,
            IDeleteTeacher deleteTeacher,
            IGetTeacherById getTeacherById,
            IGetAllTeachers getAllTeachers,
            IUpdateTeacher UpdateTeacher,
            IShowTeacher showTeacher)
        {
            _addTeacher = addTeacher;
            _deleteTeacher = deleteTeacher;
            _getTeacherById = getTeacherById;
            _getAllTeachers = getAllTeachers;
            _UpdateTeacher = UpdateTeacher;
            _showTeacher = showTeacher;
        }

        public async Task TeacherMenuMain()
        {
            while (true)
            {
                Console.Clear();
                Header("Teacher Menu");
                Console.WriteLine("1. Add Teachers");
                Console.WriteLine("2. Update Teachers");
                Console.WriteLine("3. Delete Teachers");
                Console.WriteLine("4. Show All Teachers");
                Console.WriteLine("5. Search By National Code");
                Console.WriteLine("0. Go To Main Menu");
                LineUi();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": await AddTeachers(); break;
                    case "2": await UpdateTeachers(); break;
                    case "3": await DeleteTeachers(); break;
                    case "4": ShowAllTeachers(); break;
                    case "5": SearchByNationalCode(); break;
                    case "0": return;
                    default: ControlInput(); break;
                }
            }
        }

        public async Task AddTeachers()
        {
            Header("Adding Teacher");
            Console.WriteLine("Teacher Name: ");
            var tName = Console.ReadLine();

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

            Console.WriteLine("Expertise: ");
            var expertise = Console.ReadLine();

            await LoadingSpinner();

            var result = await _addTeacher.Execute(tName, nCode, expertise);
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

        public async Task UpdateTeachers()
        {
            Header("Update Teachers");
            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teacher have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Teachers: {teachers.Count}");
                foreach (Teacher Teacher in teachers)
                {
                    LineUi();
                    Console.WriteLine($"Id: {Teacher.ID}");
                    Console.WriteLine($"Name: {Teacher.FullName}");
                    Console.WriteLine($"National code: {Teacher.NCode}");
                    Console.WriteLine($"Expertise: {Teacher.Expertise}");
                }
                LineUi();
                Console.WriteLine("Enter the teacher ID you want to edit: ");
                var tchrId = Convert.ToInt32(Console.ReadLine());
                var teacherResult = _getTeacherById.Execute(tchrId);
                if (teacherResult == null)
                {
                    LineUi();
                    TextColor("No teacher with this ID was found.\n", "Red");
                    LineUi();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }
                else
                {
                    Header("Edit Teacher");
                    TextColor("The teacher You Want To Edit:\n", "Green");
                    Console.WriteLine($"Name: {teacherResult.FullName}\nNational code: {teacherResult.NCode}\nExpertise: {teacherResult.Expertise}");
                    LineUi();
                    Console.WriteLine("What do you want to edit?");
                    Console.WriteLine("     1. Edit Name");
                    Console.WriteLine("     2. Edit National code");
                    Console.WriteLine("     3. Edit Expertise");
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
                            var updatedNameTeacher = new Teacher
                            {
                                ID = teacherResult.ID,
                                FullName = onlyName,
                                NCode = teacherResult.NCode,
                                Expertise = teacherResult.Expertise
                            };
                            await LoadingSpinner();
                            await _UpdateTeacher.Execute(updatedNameTeacher);
                            LineUi();
                            TextColor("The teacher successfully changed\n", "Green");
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

                                var updatedNCodetTeacher = new Teacher
                                {
                                    ID = teacherResult.ID,
                                    FullName = teacherResult.FullName,
                                    NCode = onlyNCode,
                                    Expertise = teacherResult.Expertise
                                };
                                await LoadingSpinner();
                                await _UpdateTeacher.Execute(updatedNCodetTeacher);
                                LineUi();
                                TextColor("The Teacher successfully changed\n", "Green");
                                Console.WriteLine("Press Any Key To Go Back.");
                                Console.ReadKey();
                                break;
                            }

                            break;

                        case "3":
                            Header("Edit Expertise");
                            Console.WriteLine("Enter New Expertise: ");
                            var onlyExpertise = Console.ReadLine();
                            var updatedExpertiseTeacher = new Teacher
                            {
                                ID = teacherResult.ID,
                                FullName = teacherResult.FullName,
                                NCode = teacherResult.NCode,
                                Expertise = onlyExpertise
                            };
                            await LoadingSpinner();
                            await _UpdateTeacher.Execute(updatedExpertiseTeacher);
                            LineUi();
                            TextColor("The teacher successfully changed\n", "Green");
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "4":
                            Header("Edit All Information");
                            Console.WriteLine("Enter New Name: ");
                            var nTchrName = Console.ReadLine();
                            string nTchrNCode;

                            while (true)
                            {
                                Console.WriteLine("Enter national code: ");
                                nTchrNCode = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nTchrNCode))
                                {
                                    TextColor("National code cannot be empty!\n", "Red");
                                    continue;
                                }

                                if (!nTchrNCode.All(char.IsDigit))
                                {
                                    TextColor("National code must contain only numbers!\n", "Red");
                                    continue;
                                }

                                if (nTchrNCode.Length != 10)
                                {
                                    TextColor("National code should be exactly 10 numbers!\n", "Yellow");
                                    continue;
                                }

                                break;
                            }
                            Console.WriteLine("Enter New Expertise: ");
                            var nTchrExpertise = Console.ReadLine();
                            var updatedTeacher = new Teacher
                            {
                                ID = teacherResult.ID,
                                FullName = nTchrName,
                                NCode = nTchrNCode,
                                Expertise = nTchrExpertise,
                            };
                            await LoadingSpinner();
                            await _UpdateTeacher.Execute(updatedTeacher);
                            LineUi();
                            TextColor("The teacher successfully changed\n", "Green");
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;
                    }

                }

            }
        }

        public async Task DeleteTeachers()
        {
            Header("Delete Teacher");
            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (teachers.Count == 0)
            {
                
                Console.WriteLine("No teacher have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine($"Total Teachers: {teachers.Count}");
                foreach (Teacher teach in teachers)
                {
                    LineUi();
                    Console.WriteLine($"Id: {teach.ID}");
                    Console.WriteLine($"Name: {teach.FullName}");
                    Console.WriteLine($"National code: {teach.NCode}");
                    Console.WriteLine($"Expertise: {teach.Expertise}");


                }
                LineUi();
                int teachDeleteId;
                bool isValidInput = false;
                do
                {
                    Console.Write("Enter the teacher ID you want to delete:");
                    TextColor(" (Or press 0 to cancel)\n", "Yellow");
                    string input = Console.ReadLine();
                    if (input == "0") return;
                    isValidInput = int.TryParse(input, out teachDeleteId) && teachDeleteId > 0;
                    if (!isValidInput)
                    {
                        TextColor("Invalid ID. Please enter a positive number.\n", "Red");
                    }
                } while (!isValidInput);

                //await LoadingSpinner();
                var teachDeleteResult = _getTeacherById.Execute(teachDeleteId);
                if (teachDeleteResult == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("No teacher with this ID was found.");
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
                    TextColor($"{teachDeleteResult.FullName}", "Red");
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
                            await _deleteTeacher.Execute(teachDeleteId);
                            LineUi();
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
        }

        public void ShowAllTeachers()
        {

            Header("Showing Teacher");

            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers have been added.");
            }
            else
            {
                Console.WriteLine($"Total teachers: {teachers.Count}");
                foreach (Teacher T in teachers)
                {
                    LineUi();
                    Console.WriteLine($"Name: {T.FullName}");
                    Console.WriteLine($"National code: {T.NCode}");
                    Console.WriteLine($"Expertise: {T.Expertise}");

                }
            }

            LineUi();
            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
        }

        public void SearchByNationalCode()
        {
            Header("Search By National Code");
            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teacher have been added.");
                LineUi();

            }
            else
            {
                Console.WriteLine("Enter The National Code:");
                string Code = Console.ReadLine();
                var teacher = _showTeacher.Execute(Code);
                LineUi();
                if (teacher.Success)
                {
                    TextColor($"{teacher.Message}\n", "Green");
                    
                }
                else
                {
                    TextColor($"{teacher.Message}\n", "Red");
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