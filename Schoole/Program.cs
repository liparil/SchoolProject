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


            .AddScoped<IAddStudentService, AddStudentService>()
            .AddScoped<IDeleteStudentService, DeleteStudentService>()
            .AddScoped<IGetStudentByIdService, GetStudentByIdService>()
            .AddScoped<IGetAllStudentsService, GetAllStudentsService>()
            .AddScoped<IUpdateStudentService, UpdateStudentService>()
            .AddScoped<IShowStudentService, ShowStudentService>()
            .AddScoped<ISearchByNationalCodeService, SearchByNationalCodeService>()


            .AddScoped<IAddTeacherService, AddTeacherService>()
            .AddScoped<IDeleteTeacherService, DeleteTeacherService>()
            .AddScoped<IGetTeacherByIdService, GetTeacherByIdService>()
            .AddScoped<IGetAllTeachersService, GetAllTeachersService>()
            .AddScoped<IUpdataTeacherService, UpdataTeacherService>()
            .AddScoped<IShowTeacherService, ShowTeacherService>()
            .AddScoped<ISearchTeacherByNationalCodeService, SearchTeacherByNationalCodeService>()


            .AddScoped<IAddCourseService, AddCourseService>()
            .AddScoped<IDeleteCourseServise, DeleteCourseService>()
            .AddScoped<IGetAllCoursesService, GetAllCoursesService>()
            .AddScoped<IGetCourseByIdService, GetCourseByIdService>()
            .AddScoped<IUpdateCourseService, UpdateCourseService>()


            .AddScoped<IAddClassroomService, AddClassroomService>()
            .AddScoped<IAddStudentToClassroomService, AddStudentToClassroomService>()
            .AddScoped<IAssignCourseToClassroomService, AssignCourseToClassroomService>()
            .AddScoped<IDeleteClassroomService, DeleteClassroomService>()
            .AddScoped<IGetAllClassroomsService, GetAllClassroomsService>()
            .AddScoped<IGetClassroomByIdService, GetClassroomByIdService>()
            .AddScoped<IUpdateClassroomService, UpdateClassroomService>()


            .AddScoped<IAddGradeService, AddGradeService>()
            .AddScoped<IGetAllStudentsService, GetAllStudentsService>()
            .AddScoped<IGetAllCoursesService, GetAllCoursesService>()
            .AddScoped<IShowReportCardService, ShowReportCardService>()


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