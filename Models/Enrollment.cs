using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Required]
        [Display(Name = "Student")]
        public int StudentID { get; set; }
        
        [ForeignKey("StudentID")]
        public Student? Student { get; set; }

        [Required]
        [Display(Name = "Course")]
        public int CourseID { get; set; }
        
        [ForeignKey("CourseID")]
        public Course? Course { get; set; }

        [Required]
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active, Completed, Withdrawn, Suspended

        [Display(Name = "Grade")]
        [StringLength(5)]
        public string? Grade { get; set; } // A, B, C, D, F

        [Display(Name = "Score")]
        [Range(0, 100)]
        public decimal? Score { get; set; }

        [Display(Name = "Completion Date")]
        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }

        [Display(Name = "Notes")]
        [StringLength(500)]
        public string? Notes { get; set; }

        [Display(Name = "Academic Year")]
        [StringLength(20)]
        public string AcademicYear { get; set; } = DateTime.Now.Year.ToString();

        [Display(Name = "Semester")]
        [StringLength(20)]
        public string Semester { get; set; } = "1";

        public bool IsActive => Status == "Active";
        public bool IsCompleted => Status == "Completed";
    }
}