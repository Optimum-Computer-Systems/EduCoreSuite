using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EduCoreSuite.Data;
using EduCoreSuite.Models;

namespace EduCoreSuite.Views
{
    public class CreateModel : PageModel
    {
        private readonly EduCoreSuite.Data.ForgeDBContext _context;

        public CreateModel(EduCoreSuite.Data.ForgeDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Campus Campus { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Campuses.Add(Campus);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
