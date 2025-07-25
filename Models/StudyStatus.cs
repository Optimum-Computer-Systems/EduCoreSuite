using System.ComponentModel.DataAnnotations;
namespace EduCoreSuite.Models
{
    public class StudyStatus
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Study status name is required")]
        [MaxLength(50, ErrorMessage = "Study status name cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s\-]+$", ErrorMessage = "Please enter a valid study status (e.g., Active, Graduated, Suspended, Deferred, Withdrawn)")]
        [Display(Name = "Study Status")]
        public string Name { get; set; } = default!;

        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,0-9]*$", ErrorMessage = "Please enter a professional description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

    }
}