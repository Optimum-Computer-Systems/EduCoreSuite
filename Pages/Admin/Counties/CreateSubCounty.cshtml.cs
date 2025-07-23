using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EduCoreSuite.Pages.Admin.Counties
{
    public class CreateSubCountyModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateSubCountyModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public SubCounty SubCounty { get; set; }
        public string CountyName { get; set; }

        public async Task<IActionResult> OnGetAsync(int countyId)
        {
            var county = await _db.Counties.FindAsync(countyId);
            if (county == null) return NotFound();

            SubCounty = new SubCounty { CountyID = countyId };
            CountyName = county.CountyName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _db.SubCounties.Add(SubCounty);
            await _db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
