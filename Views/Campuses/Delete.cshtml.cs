using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;

namespace EduCoreSuite.Views
{
    public class DeleteModel : PageModel
    {
        private readonly EduCoreSuite.Data.ForgeDBContext _context;

        public DeleteModel(EduCoreSuite.Data.ForgeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Campus Campus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses.FirstOrDefaultAsync(m => m.Id == id);

            if (campus == null)
            {
                return NotFound();
            }
            else
            {
                Campus = campus;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses.FindAsync(id);
            if (campus != null)
            {
                Campus = campus;
                _context.Campuses.Remove(Campus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
