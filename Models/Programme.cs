using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Programme
    {
        [Key]
        public int ProgrammeID { get; set; }

        [Required(ErrorMessage = "Programme name is required")]
        [StringLength(100, ErrorMessage = "Programme name can't exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department.")]
        public int DepartmentID { get; set; }

        // Navigation property: EF Core will auto-recognize the FK
        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }
    }
}
