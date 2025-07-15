using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        [Required(ErrorMessage = "Faculty name is required")]
        [StringLength(100, ErrorMessage = "Faculty name cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,]+$", ErrorMessage = "Please enter a professional faculty name (e.g., School of Engineering, Faculty of Business, School of Computing)")]
        [Display(Name = "Faculty Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,0-9]*$", ErrorMessage = "Please enter a professional description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        // Navigation
        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}
