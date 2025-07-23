using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Pages.Institutions
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Institution Institution { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Institution = await _context.Institutions
                .Include(i => i.County)
                .Include(i => i.SubCounty)
                .FirstOrDefaultAsync(m => m.InstitutionID == id);

            if (Institution == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
