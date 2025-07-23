using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;

namespace EduCoreSuite.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly ForgeDBContext _context;
        
        public FacultiesController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Faculties
        public async Task<IActionResult> Index(string searchTerm)
        {
            var facultiesQuery = _context.Faculties.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                facultiesQuery = facultiesQuery.Where(f => 
                    f.Name.Contains(searchTerm) ||
                    (f.Description != null && f.Description.Contains(searchTerm)));
            }

            var faculties = await facultiesQuery.OrderBy(f => f.Name).ToListAsync();
            
            // Pass search term to view
            ViewBag.SearchTerm = searchTerm;
            
            return View(faculties);
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.FacultyID == id);
            if (faculty == null) return NotFound();

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create() => View();

        // POST: Faculties/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Faculty faculty)
        {
            // Check for duplicate Faculty Name
            if (await _context.Faculties.AnyAsync(f => f.Name.ToLower() == faculty.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A faculty with this name already exists.");
            }

            if (!ModelState.IsValid)
                return View(faculty);

            _context.Add(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var faculty = await _context.Faculties.FindAsync(id);
            return faculty == null ? NotFound() : View(faculty);
        }

        // POST: Faculties/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyID,Name,Description")] Faculty faculty)
        {
            if (id != faculty.FacultyID) return NotFound();

            // Check for duplicate Faculty Name (excluding current faculty)
            if (await _context.Faculties.AnyAsync(f => f.FacultyID != id && f.Name.ToLower() == faculty.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A faculty with this name already exists.");
            }

            if (!ModelState.IsValid)
                return View(faculty);

            try
            {
                _context.Update(faculty);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Faculties.Any(e => e.FacultyID == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.FacultyID == id);
            if (faculty == null) return NotFound();

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX endpoint for real-time duplicate checking
        [HttpGet]
        public async Task<IActionResult> CheckDuplicateName(string name, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Faculties.AnyAsync(f => f.FacultyID != excludeId && f.Name.ToLower() == name.ToLower());
            }
            else
            {
                isDuplicate = await _context.Faculties.AnyAsync(f => f.Name.ToLower() == name.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A faculty with this name already exists." : "" });
        }


    }
}
