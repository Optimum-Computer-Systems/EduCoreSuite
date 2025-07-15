using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Data
{
    public class ForgeDBContext : DbContext
    {
        public ForgeDBContext(DbContextOptions<ForgeDBContext> options)
            : base(options)
        {
        }

        // ---------- DbSet properties ----------
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; } //  NEW
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<ExamBody> ExamBodies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<StudyMode> StudyModes { get; set; }
        public DbSet<StudyStatus> StudyStatuses { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Staff> Staff { get; set; }

        // ---------- Fluent API / seed data ----------
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // ---------- Table mapping ----------
            mb.Entity<Student>().ToTable("Students");
            mb.Entity<Course>().ToTable("Courses");
            mb.Entity<Enrollment>().ToTable("Enrollments"); //  NEW
            mb.Entity<ExamBody>().ToTable("ExamBodies");
            mb.Entity<Department>().ToTable("Departments");
            mb.Entity<Programme>().ToTable("Programmes");
            mb.Entity<Campus>().ToTable("Campuses");
            mb.Entity<StudyMode>().ToTable("StudyModes");
            mb.Entity<StudyStatus>().ToTable("StudyStatuses");
            mb.Entity<Faculty>().ToTable("Faculties");
            mb.Entity<Staff>().ToTable("Staff");

            // ---------- Relationships ----------
            mb.Entity<Enrollment>()
              .HasOne(e => e.Student)
              .WithMany()
              .HasForeignKey(e => e.StudentID);

            mb.Entity<Enrollment>()
              .HasOne(e => e.Course)
              .WithMany(c => c.Enrollments)
              .HasForeignKey(e => e.CourseID);

            // ---------- Seed reference data ----------

            mb.Entity<StudyMode>().HasData(
                new StudyMode { Id = 1, Name = "Full‑Time", Description = "Daytime attendance on campus" },
                new StudyMode { Id = 2, Name = "Part‑Time", Description = "Evening / weekend attendance" },
                new StudyMode { Id = 3, Name = "Distance Learning", Description = "Remote / online learning" }
            );

            mb.Entity<StudyStatus>().HasData(
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
