using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class SystemActivity
    {
        [Key]
        public int ActivityID { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string ActivityType { get; set; } = string.Empty; // Student, Course, Campus, Staff, System
        
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        
        public string? UserId { get; set; }
        
        public string? UserName { get; set; }
        
        // Optional related entity IDs
        public int? StudentID { get; set; }
        public int? CourseID { get; set; }
        public int? CampusID { get; set; }
        public int? StaffID { get; set; }
        
        // Icon information for display
        public string Icon { get; set; } = "fas fa-info-circle";
        public string IconColor { get; set; } = "text-primary";
    }
}