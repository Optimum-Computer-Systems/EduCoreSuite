using EduCoreSuite.Data;
using Microsoft.AspNetCore.Mvc;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCoreSuite.Pages.Auth
{
    public class SignupModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        [BindProperty]
        public User User { get; set; }
        public SignupModel(ApplicationDbContext db)
        {
            _db = db;        
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
    
            //}
            _db.Add(User);
            _db.SaveChanges();
            return Page();
        }
    }
}
