using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Pages
{
    public class InstitutionModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public InstitutionModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Institution Institution { get; set; } = new Institution();

        public List<Institution> Institutions { get; set; } = new List<Institution>();
        public List<SelectListItem> Counties { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> SubCounties { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadData();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Log incoming model
            Console.WriteLine($"Received: {Institution?.InstitutionName}, CountyID: {Institution?.CountyID}");
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            try
            {
                if (Institution.InstitutionID == 0)
                {
                    // Create new institution
                    _db.Institutions.Add(Institution);
                }
                else
                {
                    // Update existing institution
                    _db.Institutions.Update(Institution);
                }

                await _db.SaveChangesAsync();

                // Reset form after successful save
                Institution = new Institution();
                await LoadData();

                TempData["Success"] = "Institution saved successfully!";
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving institution: " + ex.Message);
                await LoadData();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            Institution = await _db.Institutions.FindAsync(id);
            if (Institution == null)
            {
                TempData["Error"] = "Institution not found!";
                Institution = new Institution();
            }

            await LoadData();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var institution = await _db.Institutions.FindAsync(id);
                if (institution != null)
                {
                    _db.Institutions.Remove(institution);
                    await _db.SaveChangesAsync();
                    TempData["Success"] = "Institution deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Institution not found!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting institution: " + ex.Message;
            }

            await LoadData();
            return Page();
        }

        // API endpoint for getting subcounties by county (AJAX call)
        public async Task<IActionResult> OnGetSubCountiesAsync(int countyId)
        {
            try
            {
                var subCounties = await _db.SubCounties
                    .Where(sc => sc.CountyID == countyId)
                    .Select(sc => new { value = sc.SubCountyID, text = sc.SubCountyName })
                    .ToListAsync();

                return new JsonResult(subCounties);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }

        private async Task LoadData()
        {
            try
            {
                // Load institutions with related county and subcounty data
                Institutions = await _db.Institutions
                    .Include(i => i.County)
                    .Include(i => i.SubCounty)
                    .OrderBy(i => i.InstitutionName)
                    .ToListAsync();

                // Load counties for dropdown
                Counties = await _db.Counties
                    .OrderBy(c => c.CountyName)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CountyID.ToString(),
                        Text = c.CountyName
                    })
                    .ToListAsync();

                // Load all subcounties for initial page load
                // Note: In practice, this will be filtered by JavaScript based on county selection
                var allSubCounties = await _db.SubCounties
                    .Include(sc => sc.County)
                    .OrderBy(sc => sc.SubCountyName)
                    .ToListAsync();

                SubCounties = allSubCounties
                    .Select(sc => new SelectListItem
                    {
                        Value = sc.SubCountyID.ToString(),
                        Text = sc.SubCountyName,
                        Group = new SelectListGroup
                        {
                            Name = sc.County != null ? sc.County.CountyName : "Unknown County"
                        }
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log error or handle appropriately
                TempData["Error"] = "Error loading data: " + ex.Message;

                // Initialize empty lists to prevent null reference errors
                Institutions = new List<Institution>();
                Counties = new List<SelectListItem>();
                SubCounties = new List<SelectListItem>();
            }
        }
    }
}