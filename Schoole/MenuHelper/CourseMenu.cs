using Schoole.Models;
using Schoole.Services.CourseService;
using Schoole.Services.TeacherService;

namespace Schoole.MenuHelper
{
    public class CourseMenu
    {
        private readonly IAddCourse _addCourse;
        private readonly IDeleteCourse _deleteCourse;
        private readonly IGetAllCourses _getAllCourses;
        private readonly IUpdateCourse _updateCourse;
        private readonly IGetAllTeachers _getAllTeachers;
        private readonly IGetCourseById _getCourseById;

        public CourseMenu(
            IAddCourse addCourse,
            IDeleteCourse deleteCourseServise,
            IGetAllCourses getAllCourses,
            IUpdateCourse updateCourse,
            IGetAllTeachers getAllTeachers,
            IGetCourseById getCourseById)
        {
            _addCourse = addCourse;
            _deleteCourse = deleteCourseServise;
            _getAllCourses = getAllCourses;
            _updateCourse = updateCourse;
            _getAllTeachers = getAllTeachers;
            _getCourseById = getCourseById;
        }

        public void CourseMenuMain()
        {
            while (true)
            {
                Header("Course Menu");
                LineUi();
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Update Course");
                Console.WriteLine("3. Delete Course");
                Console.WriteLine("4. Show All Courses");
                Console.WriteLine("0. Go To Main Menu");
                LineUi();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        bool result = AddCourse();
                        if (result)
                        {
                            while (true)
                            {
                                LineUi();
                                Console.WriteLine("If you want to add a new course, press 1.");
                                Console.WriteLine("To go back press 0");
                                int close = Convert.ToInt16(Console.ReadLine());
                                if (close == 1)
                                {
                                    AddCourse();
                                }
                                else if (close == 0)
                                {
                                    break;
                                }
                            }
                        }

                        break;
                    case "2": UpdateCourse(); break;
                    case "3": DeleteCourse(); break;
                    case "4": ShowAllCourses(); break;
                    case "0": return;
                    default: ControlInput(); break;
                }
            }
        }

        public bool AddCourse()
        {
            Header("Adding Course");
            List<Teacher> teachers = _getAllTeachers.Execute();
            LineUi();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (teachers.Count == 0)
            {
                Console.WriteLine("You can not add course without teacher. Add teacher.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return false;

            }
            else
            {
                Console.WriteLine($"Total teachers: {teachers.Count}");

                foreach (Teacher teacher in teachers)
                {
                    LineUi();
                    Console.WriteLine($"Id: {teacher.ID}");
                    Console.WriteLine($"Name: {teacher.FullName}");
                    Console.WriteLine($"National code: {teacher.NCode}");
                    Console.WriteLine($"Expertise: {teacher.Expertise}");

                }
                LineUi();
                Console.WriteLine("Course Name: ");
                var title = Console.ReadLine();
                Console.WriteLine("TecherId: ");
                var teacherId = Convert.ToInt32(Console.ReadLine());

                var result = _addCourse.Execute(title, teacherId);
                LineUi();

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
                return true;
            }
        }

        public void UpdateCourse()
        {
            Header("Update Course");
            List<Course> course = _getAllCourses.Execute();
            LineUi();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (course.Count == 0)
            {
                Console.WriteLine("No Course have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Course: {course.Count}");
                foreach (Course c in course)
                {
                    LineUi();
                    Console.WriteLine($"Id: {c.ID}");
                    Console.WriteLine($"Name: {c.Title}");
                    Console.WriteLine($"Teacher: {c.teacher.FullName}");

                }
                LineUi();
                Console.WriteLine("Enter the course ID you want to edit: ");
                var courseId = Convert.ToInt32(Console.ReadLine());
                var courseResult = _getCourseById.Execute(courseId);
                if (courseResult == null)
                {
                    LineUi();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No course with this ID was found.");
                    LineUi();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }
                else
                {
                    Header("Edit Course");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The course You Want To Edit:");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {courseResult.Title}\nTeacher: {courseResult.teacher.FullName}");
                    LineUi();
                    Console.WriteLine("What do you want to edit?");
                    Console.WriteLine("     1. Edit Title");
                    Console.WriteLine("     2. Edit Teacher");
                    Console.WriteLine("     3. Edit All Information");
                    Console.WriteLine("     0. Cancel");
                    LineUi();
                    Console.Write("Your choice: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Header("Edit Title");
                            Console.WriteLine("Enter New Title: ");
                            var onlyTitle = Console.ReadLine();
                            var updatedTitleCourse = new Course
                            {
                                ID = courseResult.ID,
                                Title = onlyTitle,
                                teacher = courseResult.teacher

                            };
                            _updateCourse.Execute(updatedTitleCourse);
                            LineUi();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The course successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "2":
                            Header("Edit Teacher");
                            List<Teacher> teachers = _getAllTeachers.Execute();
                            Console.WriteLine($"Total teachers: {teachers.Count}");
                            foreach (Teacher teacher in teachers)
                            {
                                LineUi();
                                Console.WriteLine($"Id: {teacher.ID}");
                                Console.WriteLine($"Name: {teacher.FullName}");
                                Console.WriteLine($"National code: {teacher.NCode}");
                                Console.WriteLine($"Expertise: {teacher.Expertise}");

                            }
                            var selectedCourse = _getCourseById.Execute(courseResult.ID);
                            LineUi();
                            Console.WriteLine("Enter New Teacher Id: ");
                            var onlyTeacherId = Convert.ToInt32(Console.ReadLine());

                            selectedCourse.TeacherId = onlyTeacherId;
                            _updateCourse.Execute(selectedCourse);
                            LineUi();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The course successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "3":
                            Header("Edit All Information");
                            List<Teacher> t = _getAllTeachers.Execute();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Total teachers: {t.Count}");
                            Console.ResetColor();
                            foreach (Teacher teacher in t)
                            {
                                LineUi();
                                Console.WriteLine($"Id: {teacher.ID}");
                                Console.WriteLine($"Name: {teacher.FullName}");
                                Console.WriteLine($"National code: {teacher.NCode}");
                                Console.WriteLine($"Expertise: {teacher.Expertise}");

                            }
                            LineUi();
                            var Mycourse = _getCourseById.Execute(courseResult.ID);
                            Console.WriteLine("Enter New Title: ");
                            var nTitle = Console.ReadLine();
                            Console.WriteLine("Enter New Teacher Id: ");
                            var nTeacher = Convert.ToInt32(Console.ReadLine());


                            Mycourse.Title = nTitle;
                            Mycourse.TeacherId = nTeacher;


                            _updateCourse.Execute(Mycourse);
                            LineUi();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The course successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                    }

                }

            }
        }

        public void DeleteCourse()
        {
            Header("Delete Course");
            List<Course> courses = _getAllCourses.Execute();
            LineUi();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (courses.Count == 0)
            {
                Console.WriteLine("No Course have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total courses: {courses.Count}");
                foreach (Course course in courses)
                {
                    LineUi();
                    Console.WriteLine($"Id: {course.ID}");
                    Console.WriteLine($"Name: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");


                }
                LineUi();
                Console.WriteLine("Enter the Course ID you want to delete: ");
                var courseDeleteId = Convert.ToInt32(Console.ReadLine());
                var courseDeleteResult = _getCourseById.Execute(courseDeleteId);
                var makeSure = 0;
                while (true)
                {
                    LineUi();
                    Console.Write($"Are you sure you want to delete ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{courseDeleteResult.Title}");
                    Console.ResetColor();
                    Console.WriteLine("?");
                    Console.Write("1. ");
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
                    makeSure = Convert.ToInt32(Console.ReadLine());
                    if (makeSure == 1)
                    {
                        LineUi();
                        _deleteCourse.Execute(courseDeleteId);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The course successfully deleted");
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

        public void ShowAllCourses()
        {
            Header("Showing Courses");
            List<Course> courses = _getAllCourses.Execute();
            LineUi();

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (courses.Count == 0)
            {
                Console.WriteLine("No Course have been added.");
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total courses: {courses.Count}");
                foreach (Course course in courses)
                {
                    LineUi();
                    Console.WriteLine($"Name: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");


                }
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
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
        }

        private void LineUi()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
        }
    }
}
