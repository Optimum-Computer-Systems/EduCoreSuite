using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Title { get; set; } // e.g., "Dr.", "Prof.", "Mr."

        [StringLength(50)]
        public string? StaffNumber { get; set; } // Institutional unique ID

        [Required]
        [StringLength(100)]
        public string Role { get; set; } = "Lecturer"; // e.g., Lecturer, Dean, Registrar, HOD

        // === Metadata ===

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; } = "system";

        [StringLength(100)]
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        // === Navigation ===

        public ICollection<Department> DepartmentsLed { get; set; } = new List<Department>();
    }
}
