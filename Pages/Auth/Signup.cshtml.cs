using EduCoreSuite.Data;
using Microsoft.AspNetCore.Mvc;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Pages.Auth
{
    public class SignupModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        [BindProperty]
        public new User User { get; set; }
        public SignupModel(ApplicationDbContext db)
        {
            _db = db;        
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _db.Users.AnyAsync(u => u.Username == User.Username))
            {
                ModelState.AddModelError("User.Username", "Username already taken");
                return Page();
            }

            if (await _db.Users.AnyAsync(u => u.Email == User.Email))
            {
                ModelState.AddModelError("User.Email", "Email already taken");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Add(User);
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}
