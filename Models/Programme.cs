using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Programme
    {
        [Key]
        public int ProgrammeID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Academic Level")]
        public AcademicLevel Level { get; set; }

        [Display(Name = "Accredited By")]
        [StringLength(100)]
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
