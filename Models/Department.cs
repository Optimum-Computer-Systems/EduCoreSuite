using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Department
    {
        public bool IsDeleted { get; set; } = false;
        [Key]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,]+$", ErrorMessage = "Please enter a professional department name (e.g., Computer Science, Business Administration, Information Technology)")]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department code is required")]
        [StringLength(20, ErrorMessage = "Department code cannot exceed 20 characters")]
        [RegularExpression(@"^[A-Z][A-Z0-9]*$", ErrorMessage = "Please enter a valid department code (e.g., CS, IT, BUS, COMP)")]
        [Display(Name = "Department Code")]
        public string Code { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,0-9]*$", ErrorMessage = "Please enter a professional description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a faculty.")]
        public int FacultyID { get; set; }

        [ForeignKey(nameof(FacultyID))]
        public Faculty? Faculty { get; set; }

        public ICollection<Programme> Programmes { get; set; } = new List<Programme>();

        public ICollection<Staff> DepartmentHeads { get; set; } = new List<Staff>();

        // === Status ===
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
