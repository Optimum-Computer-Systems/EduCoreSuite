using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Programme
    {
        [Key]
        public int ProgrammeID { get; set; }

        [Required(ErrorMessage = "Programme name is required")]
        [StringLength(100, ErrorMessage = "Programme name cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,]+$", ErrorMessage = "Please enter a professional programme name (e.g., Bachelor of Computer Science, Diploma in Business Management)")]
        [Display(Name = "Programme Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Programme code is required")]
        [StringLength(10, ErrorMessage = "Programme code cannot exceed 10 characters")]
        [RegularExpression(@"^[A-Z][A-Z0-9]*$", ErrorMessage = "Please enter a valid programme code (e.g., BCS, DBA, CIT)")]
        [Display(Name = "Programme Code")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Academic Level")]
        public AcademicLevel Level { get; set; }

        [Display(Name = "Accredited By")]
        [StringLength(100, ErrorMessage = "Accredited by cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,]+$", ErrorMessage = "Please enter a valid accrediting body name (e.g., Commission for University Education, KNEC)")]
        public string? AccreditedBy { get; set; }

        [Display(Name = "Accreditation Date")]
        [DataType(DataType.Date)]
        public DateTime? AccreditationDate { get; set; }

        [Display(Name = "Duration (Years)")]
        [Range(1, 10)]
        public int DurationYears { get; set; }

        [Display(Name = "Semesters Per Year")]
        [Range(1, 4)]
        public int SemestersPerYear { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Date Created")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Last Updated")]
        public DateTime UpdatedAt { get; set; } =DateTime.UtcNow;

        [Display(Name = "Created By")]
        [StringLength(100, ErrorMessage = "Created by cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s.]*$", ErrorMessage = "Please enter a valid name")]
        public string? CreatedBy { get; set; }

        [Required]
        [Display(Name = "Department")]
        [Range(1, int.MaxValue)]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }
    }

    public enum AcademicLevel
    {
        Certificate = 1,
        Diploma = 2,
        Undergraduate = 3,
        Postgraduate = 4,
        Masters = 5,
        Doctorate = 6
    }
}
