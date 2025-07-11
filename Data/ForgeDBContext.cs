using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Models;
namespace EduCoreSuite.Data
{
    public class ForgeDBContext : DbContext
    {
        public ForgeDBContext(DbContextOptions<ForgeDBContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ExamBody> ExamBodies { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Programme> Programme { get; set; } = default!;
        public DbSet<Campus> Campuses { get; set; }


    }
}
