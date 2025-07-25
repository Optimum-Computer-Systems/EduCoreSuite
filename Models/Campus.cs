using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Campus
    {
        public int Id { get; set; } // Auto-increment primary key

        [Required(ErrorMessage = "Campus code is required")]
        [StringLength(10, ErrorMessage = "Code cannot exceed 10 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Code must start with a letter and contain only letters and numbers (e.g., MAIN, CBD, KAREN)")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campus name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s]+$", ErrorMessage = "Please enter a professional campus name (e.g., Main Campus, Nairobi Campus)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "County is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid County.")]
        public int CountyID { get; set; }

        [Required(ErrorMessage = "Sub-county is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Sub-county.")]
        public int SubCountyID { get; set; }
        
        // Navigation properties
        public CountySubCounty? County { get; set; }
        public SubCounty? SubCounty { get; set; }

        [Required(ErrorMessage = "Town is required")]
        [StringLength(50, ErrorMessage = "Town name cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s\-]*$", ErrorMessage = "Please enter a valid town name")]
        public string Town { get; set; } = string.Empty;

        [Required(ErrorMessage = "Physical address is required")]
        [StringLength(150, ErrorMessage = "Physical address cannot exceed 150 characters")]
        [RegularExpression(@"^[A-Za-z0-9][A-Za-z0-9\s,.\-/]*$", ErrorMessage = "Please enter a valid physical address")]
        public string PhysicalAddress { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [RegularExpression(@"^(\+254|0)[17]\d{8}$", ErrorMessage = "Please enter a valid Kenyan phone number (e.g., 0712345678 or +254712345678)")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Url(ErrorMessage = "Please enter a valid website URL")]
        public string? WebsiteURL { get; set; }

        [StringLength(100, ErrorMessage = "Postal address cannot exceed 100 characters")]
        public string? PostalAddress { get; set; }

        [StringLength(100, ErrorMessage = "Principal name cannot exceed 100 characters")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s.]*$", ErrorMessage = "Please enter a valid principal name")]
        public string? PrincipalName { get; set; }

        [StringLength(20, ErrorMessage = "TVET registration number cannot exceed 20 characters")]
        [RegularExpression(@"^[A-Za-z0-9/\-]*$", ErrorMessage = "Please enter a valid TVET registration number")]
        public string? TVETRegistrationNumber { get; set; }

        [StringLength(10, ErrorMessage = "KUCCPS code cannot exceed 10 characters")]
        [RegularExpression(@"^[A-Za-z0-9]*$", ErrorMessage = "Please enter a valid KUCCPS code")]
        public string? KUCCPSCode { get; set; }

        public bool IsMainCampus { get; set; }

        public bool IsActive { get; set; } = true;
    }

}