using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Pages.Institutions
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Institution = await _context.Institutions.FindAsync(id);

            if (Institution != null)
            {
                _context.Institutions.Remove(Institution);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
