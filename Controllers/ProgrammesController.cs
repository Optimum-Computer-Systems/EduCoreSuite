using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Controllers
{
    public class ProgrammesController(ForgeDBContext context) : Controller
    {
        private readonly ForgeDBContext _context = context;

        public async Task<IActionResult> Index()
        {
            var programmes = await _context.Programmes
                .Include(p => p.Department)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return View(programmes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var programme = await _context.Programmes
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.ProgrammeID == id);

            return programme is null ? NotFound() : View(programme);
        }

        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(
                _context.Departments
                    .Where(d => d.IsActive && !d.IsDeleted)
                    .OrderBy(d => d.Name),
                "DepartmentID",
                "Name"
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,Level,AccreditedBy,AccreditationDate,DurationYears,SemestersPerYear,IsActive,DepartmentID")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                programme.CreatedAt = programme.UpdatedAt = DateTime.UtcNow;
                programme.CreatedBy = User.Identity?.Name ?? "system";

                _context.Programmes.Add(programme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentID"] = new SelectList(
                _context.Departments
                    .Where(d => d.IsActive && !d.IsDeleted)
                    .OrderBy(d => d.Name),
                "DepartmentID",
                "Name",
                programme.DepartmentID
            );
            return View(programme);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var programme = await _context.Programmes
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.ProgrammeID == id);

            if (programme is null) return NotFound();

            ViewData["DepartmentID"] = new SelectList(
                _context.Departments
                    .Where(d => d.IsActive && !d.IsDeleted)
                    .OrderBy(d => d.Name),
                "DepartmentID",
                "Name",
                programme.DepartmentID
            );
            return View(programme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgrammeID,Name,Code,Level,AccreditedBy,AccreditationDate,DurationYears,SemestersPerYear,IsActive,DepartmentID")] Programme programme)
        {
            if (id != programme.ProgrammeID) return NotFound();

            if (ModelState.IsValid)
            {
                programme.UpdatedAt = DateTime.UtcNow;

                try
                {
                    _context.Programmes.Update(programme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Programmes.Any(e => e.ProgrammeID == programme.ProgrammeID)) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentID"] = new SelectList(
                _context.Departments
                    .Where(d => d.IsActive && !d.IsDeleted)
                    .OrderBy(d => d.Name),
                "DepartmentID",
                "Name",
                programme.DepartmentID
            );
            return View(programme);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var programme = await _context.Programmes
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.ProgrammeID == id);

            return programme is null ? NotFound() : View(programme);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programme = await _context.Programmes.FindAsync(id);
            if (programme != null)
            {
                _context.Programmes.Remove(programme);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
