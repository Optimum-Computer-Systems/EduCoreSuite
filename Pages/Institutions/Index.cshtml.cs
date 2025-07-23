using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EduCoreSuite.Pages.Institutions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Institution> Institutions { get; set; }

        public async Task OnGetAsync()
        {
            Institutions = await _context.Institutions
                .Include(i => i.County)
                .Include(i => i.SubCounty)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var institution = await _context.Institutions.FindAsync(id);

            if (institution != null)
            {
                _context.Institutions.Remove(institution); // or soft delete logic
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Institution deleted successfully.";
            }

            return RedirectToPage();
        }
    }
}
