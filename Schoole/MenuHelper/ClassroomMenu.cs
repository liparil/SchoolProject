using Schoole.Models;
using Schoole.Services.ClassroomsService;
using Schoole.Services.CourseService;
using Schoole.Services.StudentsService;

namespace Schoole.MenuHelper
{
    public class ClassroomMenu
    {
        private readonly IAddClassroomService _addClassroomService;
        private readonly IAddStudentToClassroomService _addStudentToClassroomService;
        private readonly IAssignCourseToClassroomService _assignCourseToClassroomService;
        private readonly IDeleteClassroomService _deleteClassroomService;
        private readonly IGetAllClassroomsService _getAllClassroomsService;
        private readonly IUpdateClassroomService _updateClassroomService;
        private readonly IGetClassroomByIdService _getClassroomByIdService;
        private readonly IGetAllStudentsService _getAllStudentsService;
        private readonly IGetAllCoursesService _getAllCoursesService;


        public ClassroomMenu(IAddClassroomService addClassroomService,
         IAddStudentToClassroomService addStudentToClassroomService,
         IAssignCourseToClassroomService assignCourseToClassroomService,
         IDeleteClassroomService deleteClassroomService,
         IGetAllClassroomsService getAllClassroomsService,
         IUpdateClassroomService updateClassroomService,
         IGetClassroomByIdService getClassroomByIdService,
         IGetAllStudentsService getAllStudentsService,
         IGetAllCoursesService getAllCoursesService
         )
        {
            _addClassroomService = addClassroomService;
            _addStudentToClassroomService = addStudentToClassroomService;
            _assignCourseToClassroomService = assignCourseToClassroomService;
            _deleteClassroomService = deleteClassroomService;
            _getAllClassroomsService = getAllClassroomsService;
            _updateClassroomService = updateClassroomService;
            _getClassroomByIdService = getClassroomByIdService;
            _getAllStudentsService = getAllStudentsService;
            _getAllCoursesService = getAllCoursesService;
        }

        public void ClassroomMenuMain()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("*** ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Classroom Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" ***");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("1. Add Classroom");
                Console.WriteLine("2. Update Classroom");
                Console.WriteLine("3. Delete Classroom");
                Console.WriteLine("4. Show All Classroom");
                Console.WriteLine("5. Add Student To Classroom");
                Console.WriteLine("6. Assign Course to Classroom");
                Console.WriteLine("0. Go To Main Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        AddClassroom();
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("If you want to add a new classroom, press 1.");
                            Console.WriteLine("To go back press 0");
                            int close = Convert.ToInt16(Console.ReadLine());
                            if (close == 1)
                            {
                                AddClassroom();
                            }
                            else if (close == 0)
                            {
                                break;
                            }
                        }
                        break;

                    case "2":
                        UpdateClassroom();
                        break;
                    case "3":
                        DeleteClassroom();
                        break;
                    case "4":
                        ShowAllClassroom();
                        break;
                    case "5":
                        AddStudentToClassroom();
                        break;
                    case "6":
                        AssignCoursetoClassroom();
                        break;
                    case "0":
                        return;
                    default:
                        ControlInput();
                        break;
                }
            }
        }

        public void AddClassroom()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Adding Classroom");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.WriteLine("Classroom Name: ");
            var cName = Console.ReadLine();
            var result = _addClassroomService.Execute(cName);
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

        public void UpdateClassroom()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Update Classroom");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Classroom> classrooms = _getAllClassroomsService.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (classrooms.Count == 0)
            {
                Console.WriteLine("No classroom have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total classrooms: {classrooms.Count}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                foreach (Classroom clsroom in classrooms)
                {

                    Console.WriteLine($"Id: {clsroom.ID}");
                    Console.WriteLine($"Name: {clsroom.Name}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                }
                Console.WriteLine("Enter the classroom ID you want to edit: ");

                var clsId = Convert.ToInt32(Console.ReadLine());
                var classrommResult = _getClassroomByIdService.Execute(clsId);
                if (classrommResult == null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No classroom with this ID was found.");
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
                    Console.Write("Edit Classroom");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" ***");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The Classroom You Want To Edit:");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {classrommResult.Name}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine("Enter New Name: ");
                    var nClsroomName = Console.ReadLine();
                    var updatedClassroom = new Classroom
                    {
                        ID = classrommResult.ID,
                        Name = nClsroomName,

                    };
                    _updateClassroomService.Execute(updatedClassroom);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The classroom successfully changed");
                    Console.ResetColor();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }
            }
        }

        public void DeleteClassroom()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Delete Classroom");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Classroom> classrooms = _getAllClassroomsService.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (classrooms.Count == 0)
            {
                Console.WriteLine("No classroom have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Classroom: {classrooms.Count}");
                foreach (Classroom classroom in classrooms)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {classroom.ID}");
                    Console.WriteLine($"Name: {classroom.Name}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Enter the classroom ID you want to delete: ");
                var clsDeleteId = Convert.ToInt32(Console.ReadLine());
                var classroomDeleteResult = _getClassroomByIdService.Execute(clsDeleteId);
                var makeSure = 0;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.Write($"Are you sure you want to delete ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{classroomDeleteResult.Name}");
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
                        _deleteClassroomService.Execute(clsDeleteId);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The classroom successfully deleted");
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

        public void ShowAllClassroom()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Showing Classrooms");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            List<Classroom> classrooms = _getAllClassroomsService.Execute();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            if (classrooms.Count == 0)
            {
                Console.WriteLine("No classroom have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Total classrooms: {classrooms.Count}");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                var counter = 1;
                foreach (Classroom classroom in classrooms)
                {

                    Console.WriteLine($"Classrooms Name: {classroom.Name}");

                    if (classroom.Students != null && classroom.Students.Any())
                    {
                        foreach (Student student in classroom.Students)
                        {
                            Console.WriteLine($"  Student Name: {counter}. {student.FullName}");
                            counter += 1;

                        }

                    }
                    else
                    {
                        Console.WriteLine("  No students in this classroom.");
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();

                }
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();

            }
        }

        public void AddStudentToClassroom()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Adding Student To Classroom");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();

            List<Student> students = _getAllStudentsService.Execute();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            List<Classroom> classrooms = _getAllClassroomsService.Execute();
            if (students.Count == 0 || classrooms.Count == 0)
            {
                Console.WriteLine("You cannot perform this operation unless you have students or classroom.");
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Total students: {students.Count}");
                Console.ResetColor();
                foreach (Student student in students)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Expertise: {student.BirthDate.ToShortDateString()}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Total classrooms: {classrooms.Count}");
                Console.ResetColor();
                foreach (Classroom classroom in classrooms)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {classroom.ID} Name: {classroom.Name}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

                Console.WriteLine("Student Id: ");
                var studentId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Classroom Id: ");
                var classroomId = Convert.ToInt32(Console.ReadLine());

                var result1 = _addStudentToClassroomService.Execute(studentId, classroomId);

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
                Console.ReadKey();
            }

        }

        public void AssignCoursetoClassroom()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Assign Course to Classroom");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();


            List<Course> courses = _getAllCoursesService.Execute();
            List<Classroom> classrooms = _getAllClassroomsService.Execute();

            if (courses.Count == 0 || classrooms.Count == 0)
            {
                Console.WriteLine("You cannot perform this operation unless you have courses or classroom.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Total courses: {courses.Count}");
                Console.ResetColor();
                foreach (Course course in courses)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {course.ID}");
                    Console.WriteLine($"Title: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Total classrooms: {classrooms.Count}");
                Console.ResetColor();
                foreach (Classroom classroom in classrooms)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {classroom.ID} Name: {classroom.Name}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

                Console.WriteLine("CourseId Id: ");
                var courseId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Classroom Id: ");
                var classroomId1 = Convert.ToInt32(Console.ReadLine());

                var result2 = _assignCourseToClassroomService.Execute(courseId, classroomId1);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();

                if (result2.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result2.Message);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result2.Message);
                    Console.ResetColor();
                }
                Console.ReadKey();
            }


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
