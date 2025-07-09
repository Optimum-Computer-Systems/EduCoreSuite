using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Models;

namespace EduCoreSuite.Data
{
    public class ForgeDBContext : DbContext
    {
        public ForgeDBContext(DbContextOptions<ForgeDBContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ExamBody> ExamBodies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<StudyMode> StudyModes { get; set; }
        public DbSet<StudyStatus> StudyStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // Table configurations
            mb.Entity<Student>().ToTable("Students");
            mb.Entity<Course>().ToTable("Courses");
            mb.Entity<ExamBody>().ToTable("ExamBodies");
            mb.Entity<Department>().ToTable("Departments");
            mb.Entity<Programme>().ToTable("Programmes");
            mb.Entity<Campus>().ToTable("Campuses");

            // Seed StudyModes based on Kenyan universities (Full-Time, Part-Time, Distance) :contentReference[oaicite:1]{index=1}
            mb.Entity<StudyMode>().ToTable("StudyModes").HasData(
                new StudyMode { Id = 1, Name = "Full-Time", Description = "Daytime attendance on campus" },
                new StudyMode { Id = 2, Name = "Part-Time", Description = "Evening/weekend attendance" },
                new StudyMode { Id = 3, Name = "Distance Learning", Description = "Remote/online learning" }
            );

            // Seed StudyStatuses reflecting academic progression statuses
            mb.Entity<StudyStatus>().ToTable("StudyStatuses").HasData(
                new StudyStatus { Id = 1, Name = "Active", Description = "Currently enrolled" },
                new StudyStatus { Id = 2, Name = "Completed", Description = "Graduated successfully" },
                new StudyStatus { Id = 3, Name = "Repeating", Description = "Repeating a class or year" },
                new StudyStatus { Id = 4, Name = "Withdrawn", Description = "Exited before completion" },
                new StudyStatus { Id = 5, Name = "Suspended", Description = "Temporarily barred for discipline" },
                new StudyStatus { Id = 6, Name = "Expelled", Description = "Permanently removed from programme" }
            );
        }
    }
}
