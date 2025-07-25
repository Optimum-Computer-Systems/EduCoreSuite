using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models.ViewModels.Auth
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}