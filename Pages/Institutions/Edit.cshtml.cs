using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EduCoreSuite.Pages.Institutions
{
    public class InstitutionEditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InstitutionEditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Institution Institution { get; set; }

        public SelectList Counties { get; set; }
        public SelectList SubCounties { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Institution = await _context.Institutions.FindAsync(id);

            if (Institution == null)
                return NotFound();

            Counties = new SelectList(_context.Counties.ToList(), "CountyID", "CountyName", Institution.CountyID);
            SubCounties = new SelectList(_context.SubCounties.ToList(), "SubCountyID", "SubCountyName", Institution.SubCountyID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Counties = new SelectList(_context.Counties.ToList(), "CountyID", "CountyName", Institution.CountyID);
                SubCounties = new SelectList(_context.SubCounties.ToList(), "SubCountyID", "SubCountyName", Institution.SubCountyID);
                return Page();
            }

            var institutionInDb = await _context.Institutions.FindAsync(Institution.InstitutionID);
            if (institutionInDb == null)
                return NotFound();

            // Update the fields
            institutionInDb.InstitutionName = Institution.InstitutionName;
            institutionInDb.Email = Institution.Email;
            institutionInDb.ContactNumber = Institution.ContactNumber;
            institutionInDb.CountyID = Institution.CountyID;
            institutionInDb.SubCountyID = Institution.SubCountyID;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Institution updated successfully!";
            return RedirectToPage("./Index");
        }
    }
}
