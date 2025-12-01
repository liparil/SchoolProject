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
          IShowTeacher showTeacher
           )
        {
            _addTeacher = addTeacher;
            _deleteTeacher = deleteTeacher;
            _getTeacherById = getTeacherById;
            _getAllTeachers = getAllTeachers;
            _UpdateTeacher = UpdateTeacher;
            _showTeacher = showTeacher;
        }

        public void TeacherMenuMain()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("*** ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Teacher Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" ***");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("1. Add Teachers");
                Console.WriteLine("2. Update Teachers");
                Console.WriteLine("3. Delete Teachers");
                Console.WriteLine("4. Show All Teachers");
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
                        AddTeachers();
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("If you want to add a new teacher, press 1.");
                            Console.WriteLine("To go back press 0");
                            int close = Convert.ToInt16(Console.ReadLine());
                            if (close == 1)
                            {
                                AddTeachers();
                            }
                            else if (close == 0)
                            {
                                break;
                            }
                        }
                        break;
                    case "2":
                        UpdateTeachers();

                        break;
                    case "3":
                        DeleteTeachers();
                        break;
                    case "4":
                        ShowAllTeachers();
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

        public void AddTeachers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Adding Teacher");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.WriteLine("Teacher Name: ");
            var tName = Console.ReadLine();

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

            Console.WriteLine("Expertise: ");
            var expertise = Console.ReadLine();

            var result = _addTeacher.Execute(tName, nCode, expertise);
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

        public void UpdateTeachers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Update Teachers");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teacher have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Teachers: {teachers.Count}");
                foreach (Teacher Teacher in teachers)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {Teacher.ID}");
                    Console.WriteLine($"Name: {Teacher.FullName}");
                    Console.WriteLine($"National code: {Teacher.NCode}");
                    Console.WriteLine($"Expertise: {Teacher.Expertise}");
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Enter the teacher ID you want to edit: ");
                var tchrId = Convert.ToInt32(Console.ReadLine());
                var teacherResult = _getTeacherById.Execute(tchrId);
                if (teacherResult == null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No teacher with this ID was found.");
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
                    Console.Write("Edit Teacher");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" ***");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The teacher You Want To Edit:");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {teacherResult.FullName}\nNational code: {teacherResult.NCode}\nExpertise: {teacherResult.Expertise}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine("What do you want to edit?");
                    Console.WriteLine("     1. Edit Name");
                    Console.WriteLine("     2. Edit National code");
                    Console.WriteLine("     3. Edit Expertise");
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
                            var updatedNameTeacher = new Teacher
                            {
                                ID = teacherResult.ID,
                                FullName = onlyName,
                                NCode = teacherResult.NCode,
                                Expertise = teacherResult.Expertise
                            };
                            _UpdateTeacher.Execute(updatedNameTeacher);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The teacher successfully changed");
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

                                var updatedNCodetTeacher = new Teacher
                                {
                                    ID = teacherResult.ID,
                                    FullName = teacherResult.FullName,
                                    NCode = onlyNCode,
                                    Expertise = teacherResult.Expertise
                                };
                                _UpdateTeacher.Execute(updatedNCodetTeacher);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------------------");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("The Teacher successfully changed");
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
                            Console.Write("Edit Expertise");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" ***");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("Enter New Expertise: ");
                            var onlyExpertise = Console.ReadLine();
                            var updatedExpertiseTeacher = new Teacher
                            {
                                ID = teacherResult.ID,
                                FullName = teacherResult.FullName,
                                NCode = teacherResult.NCode,
                                Expertise = onlyExpertise
                            };
                            _UpdateTeacher.Execute(updatedExpertiseTeacher);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The teacher successfully changed");
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
                            var nTchrName = Console.ReadLine();
                            string nTchrNCode;

                            while (true)
                            {
                                Console.WriteLine("Enter national code: ");
                                nTchrNCode = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nTchrNCode))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("National code cannot be empty!");
                                    Console.ResetColor();
                                    continue;
                                }

                                if (!nTchrNCode.All(char.IsDigit))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("National code must contain only numbers!");
                                    Console.ResetColor();
                                    continue;
                                }

                                if (nTchrNCode.Length != 10)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("National code should be exactly 10 numbers!");
                                    Console.ResetColor();
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
                            _UpdateTeacher.Execute(updatedTeacher);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The teacher successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;
                    }

                }

            }
        }

        public void DeleteTeachers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Delete Teacher");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teacher have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Students: {teachers.Count}");
                foreach (Teacher teach in teachers)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {teach.ID}");
                    Console.WriteLine($"Name: {teach.FullName}");
                    Console.WriteLine($"National code: {teach.NCode}");
                    Console.WriteLine($"Expertise: {teach.Expertise}");


                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Enter the teacher ID you want to delete: ");
                var teachDeleteId = Convert.ToInt32(Console.ReadLine());
                var teachDeleteResult = _getTeacherById.Execute(teachDeleteId);
                var makeSure = 0;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.Write($"Are you sure you want to delete ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{teachDeleteResult.FullName}");
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
                        _deleteTeacher.Execute(teachDeleteId);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The teacher successfully deleted");
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

        public void ShowAllTeachers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Showing Teacher");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {T.FullName}");
                    Console.WriteLine($"National code: {T.NCode}");
                    Console.WriteLine($"Expertise: {T.Expertise}");

                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.WriteLine("Press Any Key To Go Back.");
            Console.ReadKey();
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

            List<Teacher> teachers = _getAllTeachers.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teacher have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

            }
            else
            {
                Console.WriteLine("Enter The National Code:");
                string Code = Console.ReadLine();
                var teacher = _showTeacher.Execute(Code);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                if (teacher.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(teacher.Message);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(teacher.Message);
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
