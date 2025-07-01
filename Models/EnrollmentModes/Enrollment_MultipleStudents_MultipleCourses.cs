using System;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models.EnrollmentModes
{
    public class Enrollment_MultipleStudents_MultipleCourses
    {
        [Key]
        public int EnrollmentID { get; set; }

        public int StudentID { get; set; }
        public string StudentName { get; set; } = "";

        public int CourseID { get; set; }
        public string CourseName { get; set; } = "";

        public string Department { get; set; } = "";
        public string Campus { get; set; } = "";
        public string Year { get; set; } = "";

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
