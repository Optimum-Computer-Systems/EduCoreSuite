using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name can't exceed 100 characters")]
        public string Name { get; set; } = string.Empty; // ✅ initialized to avoid null warnings

        // Navigation: One Department has many Programmes
        public ICollection<Programme> Programmes { get; set; } = new List<Programme>(); // ✅ initialized
    }
}
