using EduCoreSuite.Data;
using Microsoft.AspNetCore.Mvc;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace EduCoreSuite.Pages.Auth
{
    public class SignupModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        [BindProperty]
        public User User { get; set; }
        
        //Dropdown list for roles
        public List<SelectListItem> Roles { get; set; }
        public SignupModel(ApplicationDbContext db)
        {
            _db = db;        
        }
        public void OnGet()
        {
            LoadRoles();
        }

        private void LoadRoles()
        {
            Roles = _db.Roles.Select(r => new SelectListItem
            {
                Value = r.ID.ToString(),
                Text = r.Name
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _db.Users.AnyAsync(u => u.Username == User.Username))
            {
                ModelState.AddModelError("User.Username", "Username already taken");
                LoadRoles();
                return Page();
            }

            if (await _db.Users.AnyAsync(u => u.Email == User.Email))
            {
                ModelState.AddModelError("User.Email", "Email already taken");
                LoadRoles();
                return Page();
            }

            //User.RoleID = 1; // Default to Student role, assuming RoleID 2 is for students
            Console.WriteLine(User);
            if (!ModelState.IsValid)
            {
                LoadRoles();
                return Page();
            }
            //Hashing the password
            var passwordHasher = new PasswordHasher<User>();
            User.Password = passwordHasher.HashPassword(User, User.Password);

            _db.Add(User);
            await _db.SaveChangesAsync();
            //return Page();
            return RedirectToPage("/Auth/Approval"); //Redirect to a registration successful and approval page
        }
    }
}
