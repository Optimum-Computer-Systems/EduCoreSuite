using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;

namespace EduCoreSuite.Controllers
{
    public class StaffsController : Controller
    {
        private readonly ForgeDBContext _context;

        public StaffsController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index(string searchTerm, string roleFilter, bool? statusFilter, string departmentFilter)
        {
            var staffQuery = _context.Staff
                .Include(s => s.DepartmentsLed)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                staffQuery = staffQuery.Where(s => 
                    s.FullName.Contains(searchTerm) ||
                    (s.StaffNumber != null && s.StaffNumber.Contains(searchTerm)) ||
                    s.Role.Contains(searchTerm) ||
                    (s.Title != null && s.Title.Contains(searchTerm)));
            }

            // Apply role filter
            if (!string.IsNullOrWhiteSpace(roleFilter))
            {
                staffQuery = staffQuery.Where(s => s.Role == roleFilter);
            }

            // Apply status filter (note: IsDeleted = true means inactive)
            if (statusFilter.HasValue)
            {
                staffQuery = staffQuery.Where(s => s.IsDeleted == statusFilter.Value);
            }

            // Apply department filter
            if (!string.IsNullOrWhiteSpace(departmentFilter))
            {
                staffQuery = staffQuery.Where(s => s.DepartmentsLed.Any(d => d.Name == departmentFilter));
            }

            var staff = await staffQuery.OrderBy(s => s.FullName).ToListAsync();

            // Populate filter dropdowns
            ViewBag.Departments = new SelectList(
                await _context.Departments
                    .Where(d => !d.IsDeleted)
                    .Select(d => d.Name)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToListAsync(),
                departmentFilter);

            // Pass filter values back to view
            ViewBag.SearchTerm = searchTerm;
            ViewBag.RoleFilter = roleFilter;
            ViewBag.StatusFilter = statusFilter;
            ViewBag.DepartmentFilter = departmentFilter;
            ViewBag.DepartmentFilter = departmentFilter;

            return View(staff);
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("FullName,Title,StaffNumber,Role,IsDeleted")]
            Staff staff)
        {
            // Check for duplicate Staff FullName
            if (await _context.Staff.AnyAsync(s => !s.IsDeleted && s.FullName.ToLower() == staff.FullName.ToLower()))
            {
                ModelState.AddModelError("FullName", "A staff member with this name already exists.");
            }

            // Check for duplicate Staff Number (if provided)
            if (!string.IsNullOrWhiteSpace(staff.StaffNumber) && 
                await _context.Staff.AnyAsync(s => !s.IsDeleted && s.StaffNumber != null && s.StaffNumber.ToLower() == staff.StaffNumber.ToLower()))
            {
                ModelState.AddModelError("StaffNumber", "A staff member with this staff number already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        // POST: Staffs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("StaffID,FullName,Title,StaffNumber,Role,IsDeleted")]
            Staff staff)
        {
            if (id != staff.StaffID) return NotFound();

            // Check for duplicate Staff FullName (excluding current staff)
            if (await _context.Staff.AnyAsync(s => !s.IsDeleted && s.StaffID != id && s.FullName.ToLower() == staff.FullName.ToLower()))
            {
                ModelState.AddModelError("FullName", "A staff member with this name already exists.");
            }

            // Check for duplicate Staff Number (if provided, excluding current staff)
            if (!string.IsNullOrWhiteSpace(staff.StaffNumber) && 
                await _context.Staff.AnyAsync(s => !s.IsDeleted && s.StaffID != id && s.StaffNumber != null && s.StaffNumber.ToLower() == staff.StaffNumber.ToLower()))
            {
                ModelState.AddModelError("StaffNumber", "A staff member with this staff number already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Staff.Any(e => e.StaffID == staff.StaffID))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX endpoints for real-time duplicate checking
        [HttpGet]
        public async Task<IActionResult> CheckDuplicateFullName(string fullName, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Staff.AnyAsync(s => !s.IsDeleted && s.StaffID != excludeId && s.FullName.ToLower() == fullName.ToLower());
            }
            else
            {
                isDuplicate = await _context.Staff.AnyAsync(s => !s.IsDeleted && s.FullName.ToLower() == fullName.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A staff member with this name already exists." : "" });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicateStaffNumber(string staffNumber, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(staffNumber))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Staff.AnyAsync(s => !s.IsDeleted && s.StaffID != excludeId && s.StaffNumber != null && s.StaffNumber.ToLower() == staffNumber.ToLower());
            }
            else
            {
                isDuplicate = await _context.Staff.AnyAsync(s => !s.IsDeleted && s.StaffNumber != null && s.StaffNumber.ToLower() == staffNumber.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A staff member with this staff number already exists." : "" });
        }


    }
}
