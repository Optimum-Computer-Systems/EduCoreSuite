using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        //  Foreign key to Student
        [Required]
        public int StudentID { get; set; }

        [ForeignKey("StudentID")]
        public Student? Student { get; set; }

        //  Foreign key to Course
        [Required]
        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; }

        //  Enrollment timestamp
        [Required]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        //  Optional fields for richer tracking
        public string Semester { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty;
    }
}
