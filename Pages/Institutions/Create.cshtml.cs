using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EduCoreSuite.Pages.Institutions
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Institution Institution { get; set; }

        public SelectList Counties { get; set; }
        public SelectList SubCounties { get; set; }

        public void OnGet()
        {
            Counties = new SelectList(_context.Counties.ToList(), "CountyID", "CountyName");
            SubCounties = new SelectList(_context.SubCounties.ToList(), "SubCountyID", "SubCountyName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"Saving: {Institution.InstitutionName}, {Institution.Email}, {Institution.CountyID}");

            if (!ModelState.IsValid)
            {
                // Rebind dropdowns in case of validation failure
                Counties = new SelectList(_context.Counties.ToList(), "CountyID", "CountyName");
                SubCounties = new SelectList(_context.SubCounties.ToList(), "SubCountyID", "SubCountyName");
                return Page();
            }

            _context.Institutions.Add(Institution);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Institution created successfully!";
            return RedirectToPage("./Index");
        }
    }
}
