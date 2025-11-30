using Microsoft.Extensions.DependencyInjection;
using Schoole.Data;
using Schoole.Interfaces;
using Schoole.MenuHelper;
using Schoole.Repositories.Database;
using Schoole.Services.ClassroomsService;
using Schoole.Services.CourseService;
using Schoole.Services.GradeService;
using Schoole.Services.StudentsService;
using Schoole.Services.TeacherService;

class Program
{
    static void Main(string[] args)
    {


        var serviceProvider = new ServiceCollection()

            .AddScoped<AppDbContext>()


            .AddScoped<IStudentRepository, DbStudentRepository>()
            .AddScoped<ITeacherRepository, DbTeacherRepository>()
            .AddScoped<ICourseRepository, DbCourseRepository>()
            .AddScoped<IClassroomRepository, DbClassroomRepository>()
            .AddScoped<IGradeRepository, DbGradeRepository>()


            .AddScoped<IAddStudent, AddStudent>()
            .AddScoped<IDeleteStudent, DeleteStudent>()
            .AddScoped<IGetStudentById, GetStudentById>()
            .AddScoped<IGetAllStudents, GetAllStudents>()
            .AddScoped<IUpdateStudent, UpdateStudent>()
            .AddScoped<IShowStudent, ShowStudent>()


            .AddScoped<IAddTeacher, AddTeacher>()
            .AddScoped<IDeleteTeacher, DeleteTeacher>()
            .AddScoped<IGetTeacherById, GetTeacherById>()
            .AddScoped<IGetAllTeachers, GetAllTeachers>()
            .AddScoped<IUpdataTeacher, UpdataTeacher>()
            .AddScoped<IShowTeacher, ShowTeacher>()



            .AddScoped<IAddCourse, AddCourse>()
            .AddScoped<IDeleteCourse, DeleteCourse>()
            .AddScoped<IGetAllCourses, GetAllCourses>()
            .AddScoped<IGetCourseById, GetCourseById>()
            .AddScoped<IUpdateCourse, UpdateCourse>()


            .AddScoped<IAddClassroom, AddClassroom>()
            .AddScoped<IAddStudentToClassroom, AddStudentToClassroom>()
            .AddScoped<IAssignCourseToClassroom, AssignCourseToClassroom>()
            .AddScoped<IDeleteClassroom, DeleteClassroom>()
            .AddScoped<IGetAllClassrooms, GetAllClassrooms>()
            .AddScoped<IGetClassroomById, GetClassroomById>()
            .AddScoped<IUpdateClassroom, UpdateClassroom>()


            .AddScoped<IAddGrade, AddGrade>()
            .AddScoped<IShowReportCard, ShowReportCard>()


            .AddTransient<StudentMenu>()
            .AddTransient<TeacherMenu>()
            .AddTransient<CourseMenu>()
            .AddTransient<ClassroomMenu>()
            .AddTransient<GradeMenu>()
            .AddSingleton<MenuHelper>()


            .BuildServiceProvider();

        var menuHelper = serviceProvider.GetRequiredService<MenuHelper>();
        menuHelper.MainMenu();

    }


}