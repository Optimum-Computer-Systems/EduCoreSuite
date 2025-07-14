using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty; // e.g., "CS", "MKT", "PHY"

        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        // === Relationships ===

        [Required(ErrorMessage ="Please select a faculty.")]
        public int? FacultyID { get; set; }

        [ForeignKey("FacultyID")]
        public Faculty Faculty { get; set; } = null!;

        public ICollection<Programme> Programmes { get; set; } = new List<Programme>();

        public ICollection<Staff> DepartmentHeads { get; set; } = new List<Staff>(); // optional many-to-many

        // === Metadata ===

        [Required]
        public bool IsActive { get; set; } = true; // Status: Active/Inactive

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; } = "system";

        [StringLength(100)]
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
