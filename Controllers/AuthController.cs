using Microsoft.AspNetCore.Mvc;
using EduCoreSuite.Models;
using EduCoreSuite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EduCoreSuite.Models.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;

[Route("Auth")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _db;

    public AuthController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {
        var viewModel = new RegisterViewModel(); 
        ViewBag.Roles = _db.Roles.ToList();
        return View(viewModel);
    }

    [HttpPost("Register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = _db.Roles.ToList();
            return View(viewModel);
        }
         
        if (await _db.Users.AnyAsync(u => u.Email == viewModel.Email || u.UserName == viewModel.UserName))
        {
            ModelState.AddModelError("Email", "User already exists");
            ViewBag.Roles = _db.Roles.ToList();
            return View(viewModel);
        }

        var user = new User
        {
            UserName = viewModel.UserName,
            Email = viewModel.Email,
            PasswordHash = HashPassword(viewModel.Password),
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            RoleId = viewModel.RoleId
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return RedirectToAction("Login");
    }

    [HttpGet("Login")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == viewModel.Email);
        if (user == null || !VerifyPassword(viewModel.Password, user.PasswordHash))
        {
            ModelState.AddModelError("", "Invalid credentials");
            return View(viewModel);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("CustomUserId", user.CustomUserId),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = viewModel.RememberMe });

        return RedirectToAction("Approval", "User");
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }

    [HttpGet("ForgotPassword")]
    public IActionResult ForgotPassword()
    {
        return View(new ForgotPasswordViewModel());
    }

    [HttpPost("ForgotPassword")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == viewModel.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Email not found");
            return View(viewModel);
        }

        string code = new Random().Next(100000, 999999).ToString();
        HttpContext.Session.SetString("ResetCode", code);
        HttpContext.Session.SetString("ResetEmail", viewModel.Email);
        // TODO: Send email using SMTP or SendGrid

        return RedirectToAction("VerifyCode");
    }

    [HttpGet("VerifyCode")]
    public IActionResult VerifyCode()
    {
        return View(new VerifyCodeViewModel());
    }

    [HttpPost("VerifyCode")]
    [ValidateAntiForgeryToken]
    public IActionResult VerifyCode(VerifyCodeViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var sessionCode = HttpContext.Session.GetString("ResetCode");
        if (viewModel.Code != sessionCode)
        {
            ModelState.AddModelError("", "Invalid code");
            return View(viewModel);
        }

        return RedirectToAction("ResetPassword");
    }

    [HttpGet("ResetPassword")]
    public IActionResult ResetPassword()
    {
        return View(new ResetPasswordViewModel());
    }

    [HttpPost("ResetPassword")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var email = HttpContext.Session.GetString("ResetEmail");
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return RedirectToAction("Login");
        }

        user.PasswordHash = HashPassword(viewModel.NewPassword);
        await _db.SaveChangesAsync();

        // Clear the reset session
        HttpContext.Session.Remove("ResetCode");
        HttpContext.Session.Remove("ResetEmail");

        return RedirectToAction("Login");
    }

    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}