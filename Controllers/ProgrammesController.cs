using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EduCoreSuite.Controllers
{
    public class ProgrammesController : Controller
    {
        private readonly ForgeDBContext _context;

        public ProgrammesController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Programmes
        public async Task<IActionResult> Index()
        {
            var programmes = await _context.Programmes
                .Include(p => p.Department)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return View(programmes);
        }

        // GET: Programmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var programme = await _context.Programmes
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.ProgrammeID == id);

            if (programme == null) return NotFound();
            return View(programme);
        }

        // GET: Programmes/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropdown();
            return View();
        }

        // POST: Programmes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,Level,AccreditedBy,AccreditationDate,DurationYears,SemestersPerYear,IsActive,DepartmentID")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartmentsDropdown(programme.DepartmentID);
            return View(programme);
        }

        // GET: Programmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var programme = await _context.Programmes.FindAsync(id);
            if (programme == null) return NotFound();

            PopulateDepartmentsDropdown(programme.DepartmentID);
            return View(programme);
        }

        // POST: Programmes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgrammeID,Name,Code,Level,AccreditedBy,AccreditationDate,DurationYears,SemestersPerYear,IsActive,DepartmentID")] Programme programme)
        {
            if (id != programme.ProgrammeID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programme);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Programmes.Any(e => e.ProgrammeID == programme.ProgrammeID))
                        return NotFound();
                    throw;
                }
            }

            PopulateDepartmentsDropdown(programme.DepartmentID);
            return View(programme);
        }

        // GET: Programmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var programme = await _context.Programmes
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.ProgrammeID == id);

            if (programme == null) return NotFound();
            return View(programme);
        }

        // POST: Programmes/Delete/5
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

        private void PopulateDepartmentsDropdown(object? selectedDept = null)
        {
            ViewData["DepartmentID"] = new SelectList(
                _context.Departments
                    .Where(d => d.IsActive && !d.IsDeleted)
                    .OrderBy(d => d.Name)
                    .Select(d => new { d.DepartmentID, d.Name }),
                "DepartmentID",
                "Name",
                selectedDept
            );
        }
    }
}
