using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EduCoreSuite.Controllers
{
    public class InstitutionsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InstitutionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Institutions
        public async Task<IActionResult> Index()
        {
            var institutions = await _db.Institutions
                .Include(i => i.County)
                .Include(i => i.SubCounty)
                .ToListAsync();
            return View(institutions);
        }

        // GET: Institutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var institution = await _db.Institutions
                .Include(i => i.County)
                .Include(i => i.SubCounty)
                .FirstOrDefaultAsync(m => m.InstitutionID == id);
            if (institution == null) return NotFound();

            return View(institution);
        }

        // GET: Institutions/Create
        public IActionResult Create()
        {
            ViewData["CountyID"] = new SelectList(_db.Counties, "CountyID", "CountyName");
            ViewData["SubCountyID"] = new SelectList(_db.SubCounties, "SubCountyID", "SubCountyName");
            return View();
        }

        // POST: Institutions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstitutionID,InstitutionName,Email,ContactNumber,CountyID,SubCountyID")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                _db.Add(institution);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountyID"] = new SelectList(_db.Counties, "CountyID", "CountyName", institution.CountyID);
            ViewData["SubCountyID"] = new SelectList(_db.SubCounties, "SubCountyID", "SubCountyName", institution.SubCountyID);
            return View(institution);
        }

        // GET: Institutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var institution = await _db.Institutions.FindAsync(id);
            if (institution == null) return NotFound();

            ViewData["CountyID"] = new SelectList(_db.Counties, "CountyID", "CountyName", institution.CountyID);
            ViewData["SubCountyID"] = new SelectList(_db.SubCounties, "SubCountyID", "SubCountyName", institution.SubCountyID);
            return View(institution);
        }

        // POST: Institutions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstitutionID,InstitutionName,Email,ContactNumber,CountyID,SubCountyID")] Institution institution)
        {
            if (id != institution.InstitutionID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(institution);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitutionExists(institution.InstitutionID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountyID"] = new SelectList(_db.Counties, "CountyID", "CountyName", institution.CountyID);
            ViewData["SubCountyID"] = new SelectList(_db.SubCounties, "SubCountyID", "SubCountyName", institution.SubCountyID);
            return View(institution);
        }

        // GET: Institutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var institution = await _db.Institutions
                .Include(i => i.County)
                .Include(i => i.SubCounty)
                .FirstOrDefaultAsync(m => m.InstitutionID == id);
            if (institution == null) return NotFound();

            return View(institution);
        }

        // POST: Institutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institution = await _db.Institutions.FindAsync(id);
            _db.Institutions.Remove(institution);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitutionExists(int id)
        {
            return _db.Institutions.Any(e => e.InstitutionID == id);
        }
    }
}
