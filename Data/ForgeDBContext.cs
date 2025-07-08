using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Models;
using EduCoreSuite.Models.EnrollmentModes;
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


        public DbSet<Enrollment_SingleStudent_SingleCourse> Enrollment_SingleStudent_SingleCourse { get; set; }
        public DbSet<Enrollment_MultipleStudents_SingleCourse> Enrollment_MultipleStudents_SingleCourse { get; set; }
        public DbSet<Enrollment_SingleStudent_MultipleCourses> Enrollment_SingleStudent_MultipleCourses { get; set; }
        public DbSet<Enrollment_MultipleStudents_MultipleCourses> Enrollment_MultipleStudents_MultipleCourses { get; set; }
    }
}
