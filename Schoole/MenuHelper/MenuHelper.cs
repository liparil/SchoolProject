using Schoole.Repositories;
using Schoole.Repositories.Database;
using Schoole.Services;

namespace Schoole.MenuHelper
{
    public class MenuHelper
    {

        private readonly DbStudentRepository _studentRepo;
        private readonly StudentService _studentService;


        private readonly DbTeacherRepository _teacherRepo;
        private readonly TeacherService _teacherService;


        private readonly DbCourseRepository _courseRepo;
        private readonly CourseService _courseService;


        private readonly DbClassroomRepository _classroomRepo;
        private readonly ClassroomService _classroomService;


        private readonly DbGradeRepository _gradeRepo;
        private readonly GradeService _gradeService;

        public MenuHelper(
        DbStudentRepository studentRepo, StudentService studentService,
        DbTeacherRepository teacherRepo, TeacherService teacherService,
        DbCourseRepository courseRepo, CourseService courseService,
        DbClassroomRepository classroomRepo, ClassroomService classroomService,
        DbGradeRepository gradeRepo, GradeService gradeService)
        {
            _studentRepo = studentRepo;
            _studentService = studentService;
            _teacherRepo = teacherRepo;
            _teacherService = teacherService;
            _courseRepo = courseRepo;
            _courseService = courseService;
            _classroomRepo = classroomRepo;
            _classroomService = classroomService;
            _gradeRepo = gradeRepo;
            _gradeService = gradeService;

        }

        public void MainMenu()
        {

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("*** ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Main Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" ***");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("1. Students");
                Console.WriteLine("2. Teachers");
                Console.WriteLine("3. Courses");
                Console.WriteLine("4. Classroom");
                Console.WriteLine("5. Grade");
                Console.WriteLine("0. Exit");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

                var choice = Console.ReadLine();
                choice = choice.Trim();
                switch (choice)
                {
                    case "1":
                        ShowStudentMenu();
                        break;
                    case "2":
                        ShowTeachertMenu();
                        break;
                    case "3":
                        ShowCourseMenu();
                        break;
                    case "4":
                        ShowClassroomMenu();
                        break;
                    case "5":
                        ShowGradeMenu();
                        break;
                    case "0":
                        exitMethod();
                        break;
                    default:
                        ControlInput();
                        break;

                }
            }
        }

        void ShowStudentMenu()
        {
            var studentMenu = new StudentMenu(this);
            studentMenu.Student(_studentRepo, _studentService);
        }

        void ShowTeachertMenu()
        {
            var teacherMenu = new TeacherMenu(this);
            teacherMenu.Teacher(_teacherRepo, _teacherService);

        }

        void ShowCourseMenu()
        {
            var courseMenu = new CourseMenu(this);
            courseMenu.Course(_teacherRepo, _teacherService, _courseRepo, _courseService);

        }

        void ShowClassroomMenu()
        {
            var classroomMenu = new ClassroomMenu(this);
            classroomMenu.Classroom(_classroomRepo, _classroomService, _studentRepo, _studentService, _courseRepo, _courseService, _teacherRepo, _teacherService);

        }

        void ShowGradeMenu()
        {
            var gradeMenu = new GradeMenu(this);
            gradeMenu.Grade(_gradeRepo, _gradeService, _studentRepo, _studentService, _courseRepo, _courseService);

        }

        public void ControlInput()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine($"Are you sure you want to exit?");
                Console.WriteLine();
                Console.Write("     1. ");
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

    }
}
