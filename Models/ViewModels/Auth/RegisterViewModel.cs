using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        public int RoleID { get; set; }
    }
}