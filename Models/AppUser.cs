using System;
using System.ComponentModel.DataAnnotations;

public class AppUser
{
    public int Id { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string? ResetOTP { get; set; }
    public DateTime? OTPGeneratedAt { get; set; }
}
