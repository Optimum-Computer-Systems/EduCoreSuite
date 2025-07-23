using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using System;
using EduCoreSuite.Data;

public class ChangePasswordModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ChangePasswordModel> _logger;

    public ChangePasswordModel(ApplicationDbContext db, ILogger<ChangePasswordModel> logger)
    {
        _db = db;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string Email { get; set; }
    public string ErrorMessage { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "OTP is required.")]
        public string OTP { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your new password.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public IActionResult OnGet()
    {
        Email = TempData["ResetEmail"] as string;
        if (string.IsNullOrEmpty(Email))
        {
            return RedirectToPage("ForgotPassword");
        }

        TempData.Keep("ResetEmail");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await Task.Yield();

        Email = TempData["ResetEmail"] as string;
        if (string.IsNullOrEmpty(Email))
        {
            return RedirectToPage("ForgotPassword");
        }

        TempData.Keep("ResetEmail");

        var user = _db.Users.FirstOrDefault(u => u.Email == Email);
        if (user == null)
        {
            ErrorMessage = "Invalid request.";
            return Page();
        }

        bool isVerifyOnly = Request.Form.ContainsKey("verify");

        // Validate OTP
        if (user.ResetOTP != Input.OTP ||
            user.OTPGeneratedAt == null ||
            (DateTime.UtcNow - user.OTPGeneratedAt.Value).TotalMinutes > 10)
        {
            ErrorMessage = "Invalid or expired OTP.";
            return Page();
        }

        if (isVerifyOnly)
        {
            // OTP is valid, show success and remain on page
            ModelState.Clear();
            TempData["OTPVerified"] = true;
            _logger.LogInformation($"OTP verified for {Email}.");
            return Page();
        }

        // Validate password input
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Update password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Input.NewPassword);
        user.ResetOTP = null;
        user.OTPGeneratedAt = null;

        await _db.SaveChangesAsync();

        _logger.LogInformation($"Password updated successfully for {Email}.");

        TempData["PasswordChanged"] = true; // Set flag for popup

        return Page();
    }
}
    