using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduCoreSuite.Models; // For Student and Course

namespace EduCoreSuite.Models.EnrollmentModes
{
    public class Enrollment_SingleStudent_SingleCourse
    {
        [Key]
        public int EnrollmentID { get; set; }

        public int StudentID { get; set; } // FK
        public string StudentName { get; set; } = "";

        public int CourseID { get; set; } // FK
        public string CourseName { get; set; } = "";

        public string Department { get; set; } = "";
        public string Campus { get; set; } = "";
        public string Year { get; set; } = "";

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        // ✅ Navigation properties (won't affect existing fields)
        [ForeignKey("StudentID")]
        public Student? Student { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; }
    }
}
