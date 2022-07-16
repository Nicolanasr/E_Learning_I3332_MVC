using E_Learning_I3332_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_I3332_MVC.Data
{
    public class MySqlDbContextOptionsBuilderExtensions : DbContext
    {


        public DbSet<Users>? Users { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Students>? Students { get; set; }
        public DbSet<Teachers>? Teachers { get; set; }
        public DbSet<Courses>? Courses { get; set; }
        public DbSet<Specializations>? Specializations { get; set; }
        public DbSet<Teaches>? Teaches { get; set; }
        public DbSet<StudentCourses>? StudentCourses { get; set; }

        public MySqlDbContextOptionsBuilderExtensions(DbContextOptions<MySQLDBContext> options) : base(options) { }
    }
}