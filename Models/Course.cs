using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EduCoreSuite.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Programme { get; set; } = string.Empty;

        [Required]
        public string StudyLevel { get; set; } = string.Empty;

        [Required]
        public string ExamBody { get; set; } = string.Empty;

        [Required]
        public string StudyStatus { get; set; } = string.Empty; // e.g. "Active", "Archived"

        [Required]
        public string Campus { get; set; } = string.Empty;

        // Timestamp for growth and filtering
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //   Navigation property for enrollments
        public ICollection<Enrollment>? Enrollments { get; set; }

        public Course() { }
    }
}
