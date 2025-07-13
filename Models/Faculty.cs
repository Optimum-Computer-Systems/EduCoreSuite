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
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        // Navigation
        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}
