using System;
using System.Collections.Generic;
﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Course
    {
        // Primary Key
        public int CourseID { get; set; }

        // Core Course Information
        [Required(ErrorMessage = "Course name is required")]
        [StringLength(200, ErrorMessage = "Course name cannot exceed 200 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,0-9]+$", ErrorMessage = "Please enter a professional course name (e.g., Bachelor of Computer Science, Diploma in Business Management)")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public string StudyLevel { get; set; } = string.Empty;

        [Required]
        public string StudyStatusLabel { get; set; } = "Active"; // Optional label for display purposes

        [Required]
        public string CampusLabel { get; set; } = string.Empty;

        // Foreign Keys & Navigation Properties
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        [ValidateNever]
        public Department Department { get; set; } = null!;

        [Display(Name = "Programme")]
        public int ProgrammeID { get; set; }
        [ValidateNever]
        public Programme Programme { get; set; } = null!;

        [Display(Name = "Campus")]
        public int CampusID { get; set; }
        [ValidateNever]
        public Campus Campus { get; set; } = null!;

        [Display(Name = "Exam Body")]
        public int ExamBodyID { get; set; }
        [ValidateNever]
        public ExamBody ExamBody { get; set; } = null!;

        [Display(Name = "Study Status")]
        public int StudyStatusID { get; set; }
        [ValidateNever]
        public StudyStatus StudyStatus { get; set; } = null!;

        [Display(Name = "Study Mode")]
        public int StudyModeID { get; set; }
        [ValidateNever]
        public StudyMode StudyMode { get; set; } = null!;

        // Timestamp for dashboard analytics
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for enrollments
        public ICollection<Enrollment>? Enrollments { get; set; }

        public Course() { }
    }
}
