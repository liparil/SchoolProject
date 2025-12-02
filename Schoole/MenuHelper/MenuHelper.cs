namespace Schoole.MenuHelper
{
    public class MenuHelper
    {

        private readonly StudentMenu _studentMenu;
        private readonly TeacherMenu _teacherMenu;
        private readonly CourseMenu _courseMenu;
        private readonly ClassroomMenu _classroomMenu;
        private readonly GradeMenu _gradeMenu;

        public MenuHelper(
            StudentMenu studentMenu,
            TeacherMenu teacherMenu,
            CourseMenu courseMenu,
            ClassroomMenu classroomMenu,
            GradeMenu gradeMenu)
        {
            _studentMenu = studentMenu;
            _teacherMenu = teacherMenu;
            _courseMenu = courseMenu;
            _classroomMenu = classroomMenu;
            _gradeMenu = gradeMenu;
           
        }

        public void MainMenu()
        {

            while (true)
            {
                Header("Main Menu");
                Console.WriteLine("1. Students");
                Console.WriteLine("2. Teachers");
                Console.WriteLine("3. Courses");
                Console.WriteLine("4. Classroom");
                Console.WriteLine("5. Grade");
                Console.WriteLine("0. Exit");
                LineUi();
                var choice = Console.ReadLine();
                choice = choice.Trim();
                switch (choice)
                {
                    case "1": _studentMenu.StudentMenuMain(); break;
                    case "2": _teacherMenu.TeacherMenuMain(); break;
                    case "3": _courseMenu.CourseMenuMain(); break;
                    case "4": _classroomMenu.ClassroomMenuMain(); break;
                    case "5": _gradeMenu.GradeMenuMain(); break;
                    case "0": exitMethod(); break;
                    default: ControlInput(); break;

                }
            }
        }

        public void ControlInput()
        {
            LineUi();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Your input is out of range");
            Console.ResetColor();
            Console.Write("Press any key to try again");
            Console.ReadKey();
        }

        public void exitMethod()
        {
            var exit = 0;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("   *** ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Exit Program");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" ***");
                Console.ResetColor();
                LineUi();
                Console.WriteLine($"Are you sure you want to exit?");
                Console.WriteLine();
                Console.Write("     1. ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Yes");
                Console.ResetColor();
                Console.Write("   2. ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No");
                LineUi();
                Console.Write("Select an option (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("1");
                Console.ResetColor();
                Console.Write("/");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("2");
                Console.ResetColor();
                Console.Write("): ");
                exit = Convert.ToInt32(Console.ReadLine());
                if (exit == 1)
                {
                    Environment.Exit(0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The student successfully deleted");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    break;
                }

            }
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
