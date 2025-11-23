using Microsoft.EntityFrameworkCore;
using Schoole.Models;

namespace Schoole.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Classroom> Classrooms { get; set; }

        public DbSet<Grade> Grades { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate = True;");
        }
    }
}
