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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Campus> Campuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<ExamBody>().ToTable("ExamBodies");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Programme>().ToTable("Programmes");
            modelBuilder.Entity<Campus>().ToTable("Campuses");
        }
    }
}
