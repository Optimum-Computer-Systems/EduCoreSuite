using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using System.Linq;
using EduCoreSuite.Data;
using EduCoreSuite.Pages;
using EducoreSuite.stmpservices;
namespace EducoreSuite.forgotpassword
{


    public class ForgotPasswordModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ForgotPasswordModel> _logger;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(ApplicationDbContext db, ILogger<ForgotPasswordModel> logger, IEmailSender emailSender)
        {
            _db = db;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string InfoMessage { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("ForgotPassword: OnPostAsync triggered.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid. Errors: {@Errors}, Email: {Email}",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList(), Email);
                return Page();
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                InfoMessage = "If this email exists, a reset code has been sent.";
                _logger.LogWarning("User with email {Email} not found.", Email);
                return Page();
            }

            // Generate and store OTP
            var otp = new Random().Next(100000, 999999).ToString();
            user.ResetOTP = otp;
            user.OTPGeneratedAt = DateTime.UtcNow;

            try
            {
                await _db.SaveChangesAsync();
                await _emailSender.SendEmailAsync(user.Email, "Your OTP Code", $"Your One-Time Password is: {otp}");
                _logger.LogInformation($"OTP sent to {Email}: {otp}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP.");
                ErrorMessage = "An error occurred while sending the OTP. Please try again.";
                return Page();
            }

            // Store email temporarily and redirect
            TempData["ResetEmail"] = Email;
            return RedirectToPage("ChangePassword");
        }
    }
}


