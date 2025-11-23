using Schoole.Interfaces;
using Schoole.Models;
using Schoole.Repositories;
using Schoole.Repositories.Database;
using Schoole.Services;

namespace Schoole.MenuHelper
{
    public class CourseMenu
    {
        private readonly MenuHelper _menuHelper;

        public CourseMenu(MenuHelper menuHelper)
        {
            _menuHelper = menuHelper;
        }

        public void Course(DbTeacherRepository teacherRepo, TeacherService teacherService, DbCourseRepository courseRepo, CourseService courseService)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("*** ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Course Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" ***");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Update Course");
                Console.WriteLine("3. Delete Course");
                Console.WriteLine("4. Show All Courses");
                Console.WriteLine("0. Go To Main Menu");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        bool result = AddCourse(teacherRepo, teacherService, courseRepo, courseService);
                        if (result)
                        {
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------------------");
                                Console.ResetColor();
                                Console.WriteLine("If you want to add a new course, press 1.");
                                Console.WriteLine("To go back press 0");
                                int close = Convert.ToInt16(Console.ReadLine());
                                if (close == 1)
                                {
                                    AddCourse(teacherRepo, teacherService, courseRepo, courseService);
                                }
                                else if (close == 0)
                                {
                                    break;
                                }
                            }
                        }

                        break;
                    case "2":
                        UpdateCourse(teacherRepo, teacherService, courseRepo, courseService);

                        break;
                    case "3":
                        DeleteCourse(teacherRepo, teacherService, courseRepo, courseService);

                        break;
                    case "4":

                        ShowAllCourses(teacherRepo, teacherService, courseRepo, courseService);
                        break;
                    case "0":
                        _menuHelper.MainMenu();
                        return;
                    default:
                        _menuHelper.ControlInput();
                        break;
                }
            }
        }

        public bool AddCourse(DbTeacherRepository teacherRepo, TeacherService teacherService, DbCourseRepository courseRepo, CourseService courseService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Adding Course");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();

            List<Teacher> teachers = teacherRepo.GetAllTeachers();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (teachers.Count == 0)
            {
                Console.WriteLine("You can not add course without teacher. Add teacher.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return false;

            }
            else
            {
                Console.WriteLine($"Total teachers: {teachers.Count}");

                foreach (Teacher teacher in teachers)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {teacher.ID}");
                    Console.WriteLine($"Name: {teacher.FullName}");
                    Console.WriteLine($"National code: {teacher.NCode}");
                    Console.WriteLine($"Expertise: {teacher.Expertise}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Course Name: ");
                var title = Console.ReadLine();
                Console.WriteLine("TecherId: ");
                var teacherId = Convert.ToInt32(Console.ReadLine());

                var result = courseService.AddCourse(title, teacherId);
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
                return true;
            }
        }

        public void UpdateCourse(DbTeacherRepository teacherRepo, TeacherService teacherService, DbCourseRepository courseRepo, CourseService courseService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Update Course");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Course> course = courseRepo.GetAll();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (course.Count == 0)
            {
                Console.WriteLine("No Course have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Total Course: {course.Count}");
                foreach (Course c in course)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Id: {c.ID}");
                    Console.WriteLine($"Name: {c.Title}");
                    Console.WriteLine($"Teacher: {c.teacher.FullName}");

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Enter the course ID you want to edit: ");
                var courseId = Convert.ToInt32(Console.ReadLine());
                var courseResult = courseRepo.GetCourseById(courseId);
                if (courseResult == null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No course with this ID was found.");
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
                    Console.Write("Edit Course");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" ***");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The course You Want To Edit:");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {courseResult.Title}\nTeacher: {courseResult.teacher.FullName}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine("What do you want to edit?");
                    Console.WriteLine("     1. Edit Title");
                    Console.WriteLine("     2. Edit Teacher");
                    Console.WriteLine("     3. Edit All Information");
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
                            Console.Write("Edit Title");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" ***");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("Enter New Title: ");
                            var onlyTitle = Console.ReadLine();
                            var updatedTitleCourse = new Course
                            {
                                ID = courseResult.ID,
                                Title = onlyTitle,
                                teacher = courseResult.teacher

                            };
                            courseService.UpdateCourse(updatedTitleCourse);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The course successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "2":
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
                            List<Teacher> teachers = teacherRepo.GetAllTeachers();
                            Console.WriteLine($"Total teachers: {teachers.Count}");
                            foreach (Teacher teacher in teachers)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------------------");
                                Console.ResetColor();
                                Console.WriteLine($"Id: {teacher.ID}");
                                Console.WriteLine($"Name: {teacher.FullName}");
                                Console.WriteLine($"National code: {teacher.NCode}");
                                Console.WriteLine($"Expertise: {teacher.Expertise}");

                            }
                            var selectedCourse = courseRepo.GetCourseById(courseResult.ID);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.WriteLine("Enter New Teacher Id: ");
                            var onlyTeacherId = Convert.ToInt32(Console.ReadLine());

                            selectedCourse.TeacherId = onlyTeacherId;
                            courseService.UpdateCourse(selectedCourse);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The course successfully changed");
                            Console.ResetColor();
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "3":
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
                            List<Teacher> t = teacherRepo.GetAllTeachers();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Total teachers: {t.Count}");
                            Console.ResetColor();
                            foreach (Teacher teacher in t)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("----------------------------");
                                Console.ResetColor();
                                Console.WriteLine($"Id: {teacher.ID}");
                                Console.WriteLine($"Name: {teacher.FullName}");
                                Console.WriteLine($"National code: {teacher.NCode}");
                                Console.WriteLine($"Expertise: {teacher.Expertise}");

                            }
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            var Mycourse = courseRepo.GetCourseById(courseResult.ID);
                            Console.WriteLine("Enter New Title: ");
                            var nTitle = Console.ReadLine();
                            Console.WriteLine("Enter New Teacher Id: ");
                            var nTeacher = Convert.ToInt32(Console.ReadLine());


                            Mycourse.Title = nTitle;
                            Mycourse.TeacherId = nTeacher;


                            courseService.UpdateCourse(Mycourse);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("----------------------------");
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

        public void DeleteCourse(DbTeacherRepository teacherRepo, TeacherService teacherService, DbCourseRepository courseRepo, CourseService courseService)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Delete Course");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            List<Course> courses = courseRepo.GetAll();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (courses.Count == 0)
            {
                Console.WriteLine("No Course have been added.");
                Console.ForegroundColor = ConsoleColor.Blue;
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
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();

                    Console.WriteLine($"Id: {course.ID}");
                    Console.WriteLine($"Name: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");


                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Enter the Course ID you want to delete: ");
                var courseDeleteId = Convert.ToInt32(Console.ReadLine());
                var courseDeleteResult = courseRepo.GetCourseById(courseDeleteId);
                var makeSure = 0;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
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
                        courseService.DeleteCourse(courseDeleteId);
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

        public void ShowAllCourses(DbTeacherRepository teacherRepo, TeacherService teacherService, DbCourseRepository courseRepo, CourseService courseService)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*** ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Showing Courses");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ***");
            Console.ResetColor();
            List<Course> courses = courseRepo.GetAll();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------");
            Console.ResetColor();

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
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("----------------------------");
                    Console.ResetColor();
                    Console.WriteLine($"Name: {course.Title}");
                    Console.WriteLine($"Teacher: {course.teacher.FullName}");


                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------");
                Console.ResetColor();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
            }
        }
    }
}
