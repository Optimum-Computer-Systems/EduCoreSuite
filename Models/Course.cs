using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(200, ErrorMessage = "Course name cannot exceed 200 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,0-9]+$", ErrorMessage = "Please enter a professional course name (e.g., Bachelor of Computer Science, Diploma in Business Management)")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course code is required")]
        [StringLength(20, ErrorMessage = "Course code cannot exceed 20 characters")]
        [RegularExpression(@"^[A-Z][A-Z0-9]*$", ErrorMessage = "Please enter a valid course code (e.g., CS101, BUS200)")]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; } = string.Empty;

        // Foreign Keys & Navigation Properties
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }

        [Display(Name = "Programme")]
        public int ProgrammeID { get; set; }
        public Programme? Programme { get; set; }

        [Display(Name = "Campus")]
        public int CampusID { get; set; }
        public Campus? Campus { get; set; }

        [Display(Name = "Exam Body")]
        public int ExamBodyID { get; set; }
        public ExamBody? ExamBody { get; set; }

        [Display(Name = "Study Status")]
        public int StudyStatusID { get; set; }
        public StudyStatus? StudyStatus { get; set; }

        [Display(Name = "Study Mode")]
        public int StudyModeID { get; set; }
        public StudyMode? StudyMode { get; set; }

        [Display(Name = "Duration (Months)")]
        [Range(1, 120, ErrorMessage = "Duration must be between 1 and 120 months")]
        public int Duration { get; set; } = 12;

        [Display(Name = "Credits")]
        [Range(1, 200, ErrorMessage = "Credits must be between 1 and 200")]
        public int Credits { get; set; } = 3;

        public Course() { }
    }
}