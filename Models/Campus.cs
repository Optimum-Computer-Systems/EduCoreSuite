using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Campus
    {
        public int Id { get; set; } // Auto-increment primary key

        [Required]
        [StringLength(10, ErrorMessage = "Code cannot exceed 10 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "County name cannot exceed 50 characters.")]
        public string County { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Constituency name cannot exceed 50 characters.")]
        public string? Constituency { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Town name cannot exceed 50 characters.")]
        public string Town { get; set; } = string.Empty;

        [Required]
        [StringLength(150, ErrorMessage = "Physical address cannot exceed 150 characters.")]
        public string PhysicalAddress { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? WebsiteURL { get; set; }

        public string? PostalAddress { get; set; }

        public string? PrincipalName { get; set; }


        public string? TVETRegistrationNumber { get; set; }


        public string? KUCCPSCode { get; set; }

        public bool IsMainCampus { get; set; }

        public bool IsActive { get; set; } = true;
    }

}