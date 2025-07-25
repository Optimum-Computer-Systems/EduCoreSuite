using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ---------- DbSet properties ----------
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ExamBody> ExamBodies { get; set; }
        public DbSet<CountySubCounty> Counties { get; set; }
        public DbSet<SubCounty> SubCounties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<StudyMode> StudyModes { get; set; }
        public DbSet<StudyStatus> StudyStatuses { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<SystemActivity> Activities { get; set; }

        // Alias for backward compatibility
        public DbSet<SystemActivity> SystemActivities => Activities;

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add model configurations here if needed
            //Relationship            base.OnModelCreating(mb);

            // Table‑name mapping
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<ExamBody>().ToTable("ExamBodies");
            modelBuilder.Entity<CountySubCounty>().ToTable("Counties");
            modelBuilder.Entity<SubCounty>().ToTable("SubCounties");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Programme>().ToTable("Programmes");
            modelBuilder.Entity<Campus>().ToTable("Campuses");
            modelBuilder.Entity<StudyMode>().ToTable("StudyModes");
            modelBuilder.Entity<StudyStatus>().ToTable("StudyStatuses");
            modelBuilder.Entity<Faculty>().ToTable("Faculties");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
            modelBuilder.Entity<SystemActivity>().ToTable("Activities");

            // Seed reference data
            modelBuilder.Entity<StudyMode>().HasData(
                new StudyMode { Id = 1, Name = "Full‑Time", Description = "Daytime attendance on campus" },
                new StudyMode { Id = 2, Name = "Part‑Time", Description = "Evening / weekend attendance" },
                new StudyMode { Id = 3, Name = "Distance Learning", Description = "Remote / online learning" }
            );

            modelBuilder.Entity<StudyStatus>().HasData(
                new StudyStatus { Id = 1, Name = "Active", Description = "Currently enrolled" },
                new StudyStatus { Id = 2, Name = "Completed", Description = "Graduated successfully" },
                new StudyStatus { Id = 3, Name = "Repeating", Description = "Repeating a class or year" },
                new StudyStatus { Id = 4, Name = "Withdrawn", Description = "Exited before completion" },
                new StudyStatus { Id = 5, Name = "Suspended", Description = "Temporarily barred for discipline" },
                new StudyStatus { Id = 6, Name = "Expelled", Description = "Permanently removed from programme" }
            );

            // ⚠️ Fix for SQL Server multiple cascade paths issue
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Programme)
                .WithMany()
                .HasForeignKey(c => c.ProgrammeID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Campus>()
                .HasOne(c => c.County)
                .WithMany()
                .HasForeignKey(c => c.CountyID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Campus>()
                .HasOne(c => c.SubCounty)
                .WithMany()
                .HasForeignKey(c => c.SubCountyID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.County)
                .WithMany()
                .HasForeignKey(s => s.CountyID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.SubCounty)
                .WithMany()
                .HasForeignKey(s => s.SubCountyID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
               .HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleID);

            // Seed Roles: Admin and Student
            modelBuilder.Entity<Role>().HasData(
                new Role { ID = 1, Name = "Student", Description = "Student with limited system access" },
                new Role { ID = 2, Name = "Admin", Description = "Administrator with unlimited access" },
                new Role { ID = 3, Name = "Lecturer", Description = "Lecturer with limited system access" },
                new Role { ID = 4, Name = "Staff", Description = "Staff user with limited access" }
            );
        }
    }
}