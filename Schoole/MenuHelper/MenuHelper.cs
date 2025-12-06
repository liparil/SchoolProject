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

        public async Task MainMenuAsync()
        {

            while (true)
            {
                Console.Clear();
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
                    case "1":
                        await _studentMenu.StudentMenuMain();
                        Console.Clear();
                        break;

                    case "2":
                        await _teacherMenu.TeacherMenuMain();
                        Console.Clear();
                        break;

                    case "3":
                        await _courseMenu.CourseMenuMain();
                        Console.Clear();
                        break;

                    case "4":
                        await _classroomMenu.ClassroomMenuMain();
                        Console.Clear();
                        break;

                    case "5":
                        await _gradeMenu.GradeMenuMain();
                        Console.Clear();
                        break;

                    case "0": exitMethod(); break;

                    default: ControlInput(); break;

                }
            }
        }

        public void ControlInput()
        {
            LineUi();
            TextColor("Invalid Input!\n", "Red");

            Console.Write("Press any key to retry.");
            Console.ReadKey();
        }

        public void exitMethod()
        {
            var exit = 0;
            while (true)
            {
                Header("Exit Program");
                Console.WriteLine($"Are you sure you want to exit?");
                Console.WriteLine();
                Console.Write("     1. ");
                TextColor("Yes", "Red");
                Console.Write("   2. ");
                TextColor("No\n", "Green");
                LineUi();
                Console.Write("Select an option (");
                TextColor("1", "Red");
                Console.Write("/");
                TextColor("2", "Green");
                Console.Write("): ");
                exit = Convert.ToInt32(Console.ReadLine());
                if (exit == 1)
                    Environment.Exit(0);
                else
                    break;
                

            }
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

    }
}
