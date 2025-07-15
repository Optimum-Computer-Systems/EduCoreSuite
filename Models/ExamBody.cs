using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class ExamBody
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Exam body name is required")]
        [MaxLength(100, ErrorMessage = "Exam body name cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,]+$", ErrorMessage = "Please enter a professional exam body name (e.g., KNEC, KASNEB, CPA Kenya)")]
        [Display(Name = "Exam Body Name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s&\-().,0-9]*$", ErrorMessage = "Please enter a professional description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [MaxLength(100, ErrorMessage = "Country name cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s\-]*$", ErrorMessage = "Please enter a valid country name (e.g., Kenya, Uganda, Tanzania)")]
        [Display(Name = "Country")]
        public string? Country { get; set; }
    }
}
