using System;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty; // e.g. “Operating Systems Lecture”

        [Required]
        public DateTime ScheduledDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        // link to Course or Staff later if needed
    }
}
