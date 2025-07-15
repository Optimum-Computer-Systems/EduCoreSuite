using System.ComponentModel.DataAnnotations;
namespace EduCoreSuite.Models
{
    public class StudyMode
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Study mode name is required")]
        [MaxLength(50, ErrorMessage = "Study mode name cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s\-]+$", ErrorMessage = "Please enter a valid study mode (e.g., Full-time, Part-time, Distance Learning, Online)")]
        [Display(Name = "Study Mode")]
        public string Name { get; set; } = default!;

        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,0-9]*$", ErrorMessage = "Please enter a professional description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

    }
}