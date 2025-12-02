using Schoole.Models;
using Schoole.Services.ClassroomsService;
using Schoole.Services.CourseService;
using Schoole.Services.StudentsService;

namespace Schoole.MenuHelper
{
    public class ClassroomMenu
    {
        private readonly IAddClassroom _addClassroom;
        private readonly IAddStudentToClassroom _addStudentToClassroom;
        private readonly IAssignCourseToClassroom _assignCourseToClassroom;
        private readonly IDeleteClassroom _deleteClassroom;
        private readonly IGetAllClassrooms _getAllClassrooms;
        private readonly IUpdateClassroom _updateClassroom;
        private readonly IGetClassroomById _getClassroomById;
        private readonly IGetAllStudents _getAllStudents;
        private readonly IGetAllCourses _getAllCourses;


        public ClassroomMenu(
            IAddClassroom addClassroom,
            IAddStudentToClassroom addStudentToClassroom,
            IAssignCourseToClassroom assignCourseToClassroom,
            IDeleteClassroom deleteClassroom,
            IGetAllClassrooms getAllClassrooms,
            IUpdateClassroom updateClassroom,
            IGetClassroomById getClassroomById,
            IGetAllStudents getAllStudents,
            IGetAllCourses getAllCourses)
        {
            _addClassroom = addClassroom;
            _addStudentToClassroom = addStudentToClassroom;
            _assignCourseToClassroom = assignCourseToClassroom;
            _deleteClassroom = deleteClassroom;
            _getAllClassrooms = getAllClassrooms;
            _updateClassroom = updateClassroom;
            _getClassroomById = getClassroomById;
            _getAllStudents = getAllStudents;
            _getAllCourses = getAllCourses;
        }

        public void ClassroomMenuMain()
        {
            while (true)
            {
                Header("Classroom Menu");
                Console.WriteLine("1. Add Classroom");
                Console.WriteLine("2. Update Classroom");
                Console.WriteLine("3. Delete Classroom");
                Console.WriteLine("4. Show All Classroom");
                Console.WriteLine("5. Add Student To Classroom");
                Console.WriteLine("6. Assign Course to Classroom");
                Console.WriteLine("0. Go To Main Menu");
                LineUi();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        AddClassroom();
                        while (true)
                        {
                            LineUi();
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

                    case "2": UpdateClassroom(); break;
                    case "3": DeleteClassroom(); break;
                    case "4": ShowAllClassroom(); break;
                    case "5": AddStudentToClassroom(); break;
                    case "6": AssignCoursetoClassroom(); break;
                    case "0": return;
                    default: ControlInput(); break;
                }
            }
        }

        public void AddClassroom()
        {
            Header("Adding Classroom");
            Console.WriteLine("Classroom Name: ");
            var cName = Console.ReadLine();
            var result = _addClassroom.Execute(cName);
            LineUi();

            if (result.Success)
            {
                TextColor($"{result.Message}\n", "Green");

            }
            else
            {
                TextColor($"{result.Message}\n", "Red");
            }

        }

        public void UpdateClassroom()
        {
            Header("Update Classroom");
            List<Classroom> classrooms = _getAllClassrooms.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (classrooms.Count == 0)
            {
                Console.WriteLine("No classroom have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total classrooms: {classrooms.Count}");
                LineUi();
                foreach (Classroom clsroom in classrooms)
                {

                    Console.WriteLine($"Id: {clsroom.ID}");
                    Console.WriteLine($"Name: {clsroom.Name}");
                    LineUi();
                }
                Console.WriteLine("Enter the classroom ID you want to edit: ");

                var clsId = Convert.ToInt32(Console.ReadLine());
                var classrommResult = _getClassroomById.Execute(clsId);
                if (classrommResult == null)
                {
                    LineUi();
                    TextColor("No classroom with this ID was found.\n", "Red");
                    LineUi();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }
                else
                {
                    Header("Edit Classroom");
                    TextColor($"The Classroom You Want To Edit:\n", "Green");
                    Console.WriteLine($"Name: {classrommResult.Name}");
                    LineUi();
                    Console.WriteLine("Enter New Name: ");
                    var nClsroomName = Console.ReadLine();
                    var updatedClassroom = new Classroom
                    {
                        ID = classrommResult.ID,
                        Name = nClsroomName,

                    };
                    _updateClassroom.Execute(updatedClassroom);
                    LineUi();
                    TextColor("The classroom successfully changed\n", "Green");
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }
            }
        }

        public void DeleteClassroom()
        {
            Header("Delete Classroom");
            List<Classroom> classrooms = _getAllClassrooms.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (classrooms.Count == 0)
            {
                Console.WriteLine("No classroom have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Classroom: {classrooms.Count}");
                foreach (Classroom classroom in classrooms)
                {
                    LineUi();
                    Console.WriteLine($"Id: {classroom.ID}");
                    Console.WriteLine($"Name: {classroom.Name}");

                }
                LineUi();
                Console.WriteLine("Enter the classroom ID you want to delete: ");
                var clsDeleteId = Convert.ToInt32(Console.ReadLine());
                var classroomDeleteResult = _getClassroomById.Execute(clsDeleteId);
                var makeSure = 0;
                while (true)
                {
                    LineUi();
                    Console.Write($"Are you sure you want to delete ");
                    TextColor($"{classroomDeleteResult.Name}", "Red");
                    Console.WriteLine("?");
                    Console.Write("1. ");
                    TextColor("Yes", "Red");
                    Console.Write("   2. ");
                    TextColor("No\n", "Green");
                    LineUi();
                    Console.Write("Select an option (");
                    TextColor("1", "Red");
                    Console.Write("/");
                    TextColor("2", "Green");
                    Console.Write("): ");

                    makeSure = Convert.ToInt32(Console.ReadLine());
                    if (makeSure == 1)
                    {
                        LineUi();
                        _deleteClassroom.Execute(clsDeleteId);
                        TextColor("The classroom successfully deleted\n", "Red");
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
            Header("Show All Classroom");
            List<Classroom> classrooms = _getAllClassrooms.Execute();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            if (classrooms.Count == 0)
            {
                Console.WriteLine("No classroom have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                TextColor($"Total classrooms: {classrooms.Count}\n", "Yellow");
                LineUi();
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
                    LineUi();
                }
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();

            }
        }

        public void AddStudentToClassroom()
        {
            Header("Adding Student To Classroom");
            List<Student> students = _getAllStudents.Execute();
            LineUi();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ResetColor();
            List<Classroom> classrooms = _getAllClassrooms.Execute();
            if (students.Count == 0 || classrooms.Count == 0)
            {
                Console.WriteLine("You cannot perform this operation unless you have students or classroom.");
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                TextColor($"Total students: {students.Count}\n", "Yellow");
                foreach (Student student in students)
                {
                    LineUi();
                    Console.WriteLine($"Id: {student.ID}");
                    Console.WriteLine($"Name: {student.FullName}");
                    Console.WriteLine($"National code: {student.NCode}");
                    Console.WriteLine($"Expertise: {student.BirthDate.ToShortDateString()}");

                }
                LineUi();
                TextColor($"Total classrooms: {classrooms.Count}\n", "Yellow");
                foreach (Classroom classroom in classrooms)
                {
                    LineUi();
                    Console.WriteLine($"Id: {classroom.ID} Name: {classroom.Name}");

                }
                LineUi();

                Console.WriteLine("Student Id: ");
                var studentId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Classroom Id: ");
                var classroomId = Convert.ToInt32(Console.ReadLine());

                var result1 = _addStudentToClassroom.Execute(studentId, classroomId);
                LineUi();

                if (result1.Success)
                {
                    TextColor($"{result1.Message}\n", "Green");

                }
                else
                {
                    TextColor($"{result1.Message}\n", "Red");
                }
                Console.ReadKey();
            }

        }

        public void AssignCoursetoClassroom()
        {
            Header("Assign Course to Classroom");
            List<Course> courses = _getAllCourses.Execute();
            List<Classroom> classrooms = _getAllClassrooms.Execute();

            if (courses.Count == 0 || classrooms.Count == 0)
            {
                Console.WriteLine("You cannot perform this operation unless you have courses or classroom.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                TextColor($"Total courses: {courses.Count}\n", "Yellow");
                foreach (Course course in courses)
                {
                    LineUi();
                    Console.WriteLine($"Id: {course.ID}");
                    Console.WriteLine($"Title: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");

                }
                LineUi();
                TextColor($"Total classrooms: {classrooms.Count}", "Yellow");
                foreach (Classroom classroom in classrooms)
                {
                    LineUi();
                    Console.WriteLine($"Id: {classroom.ID} Name: {classroom.Name}");

                }
                LineUi();

                Console.WriteLine("CourseId Id: ");
                var courseId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Classroom Id: ");
                var classroomId1 = Convert.ToInt32(Console.ReadLine());

                var result2 = _assignCourseToClassroom.Execute(courseId, classroomId1);

                LineUi();

                if (result2.Success)
                {
                    TextColor($"{result2.Message}\n", "Green");

                }
                else
                {
                    TextColor($"{result2.Message}\n", "Red");
                }
                Console.ReadKey();
            }


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
    }
}
