using Schoole.Data;
using Schoole.MenuHelper;
using Schoole.Repositories.Database;
using Schoole.Services;

class Program
{
    static void Main(string[] args)
    {
        var studentRepo = new DbStudentRepository(new AppDbContext());
        var studentService = new StudentService(studentRepo);

        var teacherRepo = new DbTeacherRepository(new AppDbContext());
        var teacherService = new TeacherService(teacherRepo);

        var courseRepo = new DbCourseRepository(new AppDbContext());
        var courseService = new CourseService(courseRepo, teacherRepo);

        var classroomRepo = new DbClassroomRepository(new AppDbContext());
        var classroomService = new ClassroomService(classroomRepo, studentRepo, courseRepo, teacherRepo);

        var gradeRepo = new DbGradeRepository(new AppDbContext());
        var gradeService = new GradeService(gradeRepo, studentRepo, courseRepo);

        var menuHelper = new MenuHelper(studentRepo, studentService, teacherRepo, teacherService, courseRepo, courseService, classroomRepo, classroomService, gradeRepo, gradeService);

        menuHelper.MainMenu();
    }


}