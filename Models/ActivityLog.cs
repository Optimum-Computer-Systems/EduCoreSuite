using System;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class ActivityLog
    {
        [Key]
        public int LogID { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Category { get; set; } = string.Empty; // e.g. “Enrollment”, “Completion”, “System”
    }
}
