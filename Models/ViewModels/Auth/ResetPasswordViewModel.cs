using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models.ViewModels.Auth
{
    public class ResetPasswordViewModel
    {
        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}