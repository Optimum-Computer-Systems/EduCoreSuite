using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add model configurations here if needed
            //Relationship
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