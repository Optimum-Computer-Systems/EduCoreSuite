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
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a faculty.")]
        public int FacultyID { get; set; }

        [ForeignKey(nameof(FacultyID))]
        public Faculty Faculty { get; set; } = null!;

        public ICollection<Programme> Programmes { get; set; } = new List<Programme>();

        public ICollection<Staff> DepartmentHeads { get; set; } = new List<Staff>();

        // === Status ===
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
