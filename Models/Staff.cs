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
        [RegularExpression(@"^[A-Za-z][A-Za-z\s.'-]+$", ErrorMessage = "Please enter a valid full name (e.g., John Doe, Dr. Jane Smith)")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s.]*$", ErrorMessage = "Please enter a valid title (e.g., Dr., Prof., Mr., Ms.)")]
        [Display(Name = "Title")]
        public string? Title { get; set; } // e.g., "Dr.", "Prof.", "Mr."

        [StringLength(50, ErrorMessage = "Staff number cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z0-9/\-]+$", ErrorMessage = "Please enter a valid staff number (e.g., EMP001, STAFF/2024/001)")]
        [Display(Name = "Staff Number")]
        public string? StaffNumber { get; set; } // Institutional unique ID

        [Required(ErrorMessage = "Role is required")]
        [StringLength(100, ErrorMessage = "Role cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,]+$", ErrorMessage = "Please enter a valid role (e.g., Lecturer, Dean, Registrar, Head of Department)")]
        [Display(Name = "Role")]
        public string Role { get; set; } = "Lecturer"; // e.g., Lecturer, Dean, Registrar, HOD

         

        public bool IsDeleted { get; set; } = false;

        // === Navigation ===

        public ICollection<Department> DepartmentsLed { get; set; } = new List<Department>();
    }
}
