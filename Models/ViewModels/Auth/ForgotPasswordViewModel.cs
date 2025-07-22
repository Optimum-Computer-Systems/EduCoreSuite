using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models.ViewModels.Auth
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}