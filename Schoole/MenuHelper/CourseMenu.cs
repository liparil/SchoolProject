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

        public async Task CourseMenuMain()
        {
            while (true)
            {
                Console.Clear();
                Header("Course Menu");
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
                        bool result = await AddCourse();
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
                                    await AddCourse();
                                }
                                else if (close == 0)
                                {
                                    break;
                                }
                            }
                        }

                        break;
                    case "2": await UpdateCourse(); break;
                    case "3": await DeleteCourse(); break;
                    case "4": await ShowAllCourses(); break;
                    case "0": return;
                    default: ControlInput(); break;
                }
            }
        }

        public async Task<bool> AddCourse()
        {
            Header("Adding Course");
            List<Teacher> teachers = await _getAllTeachers.Execute();
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

                await LoadingSpinner();
                var result = await _addCourse.Execute(title, teacherId);
                LineUi();

                if (result.Success)
                {
                    TextColor($"{result.Message}\n", "Green");

                }
                else
                {
                    TextColor($"{result.Message}\n", "Red");
                }
                return true;
            }
        }

        public async Task UpdateCourse()
        {
            Header("Update Course");
            List<Course> course = await _getAllCourses.Execute();
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
                    TextColor("No course with this ID was found.\n", "Red");
                    LineUi();
                    Console.WriteLine("Press Any Key To Go Back.");
                    Console.ReadKey();
                }
                else
                {
                    Header("Edit Course");
                    TextColor("The course You Want To Edit:\n", "Green");
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
                            await LoadingSpinner();
                            await _updateCourse.Execute(updatedTitleCourse);
                            LineUi();
                            TextColor("The course successfully changed\n", "Green");
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "2":
                            Header("Edit Teacher");
                            List<Teacher> teachers = await _getAllTeachers.Execute();
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
                            await LoadingSpinner();
                            await _updateCourse.Execute(selectedCourse);
                            LineUi();
                            TextColor("The course successfully changed\n", "Green");
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                        case "3":
                            Header("Edit All Information");
                            List<Teacher> t = await _getAllTeachers.Execute();
                            TextColor($"Total teachers: {t.Count}\n", "Yellow");
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

                            await LoadingSpinner();
                            await _updateCourse.Execute(Mycourse);
                            LineUi();
                            TextColor("The course successfully changed\n", "Green");
                            Console.WriteLine("Press Any Key To Go Back.");
                            Console.ReadKey();
                            break;

                    }

                }

            }
        }

        public async Task DeleteCourse()
        {
            Header("Delete Course");
            List<Course> courses = await _getAllCourses.Execute();

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (courses.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No Course have been added.");
                LineUi();
                Console.WriteLine("Press Any Key To Go Back.");
                Console.ReadKey();
                return;
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

                int courseDeleteId;
                bool isValidInput = false;

                do
                {
                    Console.Write("Enter the Course ID you want to delete:");
                    TextColor(" (Or press 0 to cancel)\n", "Yellow");
                    string input = Console.ReadLine();

                    if (input == "0") return;

                    isValidInput = int.TryParse(input, out courseDeleteId) && courseDeleteId > 0;

                    if (!isValidInput)
                    {
                        TextColor("Invalid ID. Please enter a positive number.\n", "Red");
                    }

                } while (!isValidInput);

                var courseDeleteResult = _getCourseById.Execute(courseDeleteId);

                if (courseDeleteResult == null)
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
                    TextColor($"{courseDeleteResult.Title}", "Red");
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
                            await _deleteCourse.Execute(courseDeleteId);
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

        public async Task ShowAllCourses()
        {
            Header("Showing Courses");
            await LoadingSpinner();
            List<Course> courses = await _getAllCourses.Execute();
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

            Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");

        }
    }
}
