using Microsoft.AspNetCore.Mvc;
using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Pages.Auth
{
    public class UserProfileModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public User UserProfile { get; set; } //instance of the User model to hold user profile data

        public UserProfileModel(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == 0)
            {
                return RedirectToPage("/Auth/Signup"); // Redirect to sign up if no ID is provided
            }
            UserProfile = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.ID == id);// Check if the user exists in the database
            if (UserProfile == null)
            {
                //return NotFound();
                return RedirectToPage("/Auth/Signup"); // Redirect to sign up if no ID is provided
            }
            return Page();
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace EduCoreSuite.Pages.Auth
//{
//    public class UserProfileModel : PageModel
//    {
//        public void OnGet()
//        {
//        }
//    }
//}