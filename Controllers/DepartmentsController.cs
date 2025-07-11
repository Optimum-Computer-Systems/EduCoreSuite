using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;

namespace EduCoreSuite.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ForgeDBContext _context;

        public DepartmentsController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .Include(d => d.Faculty)
                .Include(d => d.DepartmentHeads)
                .Where(d => !d.IsDeleted)
                .ToListAsync();

            return View(departments);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments
                .Include(d => d.Faculty)
                .Include(d => d.DepartmentHeads)
                .FirstOrDefaultAsync(m => m.DepartmentID == id && !m.IsDeleted);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name");
            ViewData["StaffList"] = new MultiSelectList(_context.Staff, "StaffID", "FullName");
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department, int[] selectedStaff)
        {
            if (ModelState.IsValid)
            {
                department.CreatedAt = DateTime.UtcNow;
                department.CreatedBy = User.Identity?.Name ?? "system";
                department.IsActive = true;
                department.IsDeleted = false;

                department.DepartmentHeads = _context.Staff
                    .Where(s => selectedStaff.Contains(s.StaffID))
                    .ToList();

                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name", department.FacultyID);
            ViewData["StaffList"] = new MultiSelectList(_context.Staff, "StaffID", "FullName", selectedStaff);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments
                .Include(d => d.DepartmentHeads)
                .FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (department == null)
                return NotFound();

            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name", department.FacultyID);
            ViewData["StaffList"] = new MultiSelectList(
                _context.Staff,
                "StaffID",
                "FullName",
                department.DepartmentHeads.Select(s => s.StaffID)
            );

            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department, int[] selectedStaff)
        {
            if (id != department.DepartmentID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.Departments
                        .Include(d => d.DepartmentHeads)
                        .FirstOrDefaultAsync(d => d.DepartmentID == id);

                    if (existing == null)
                        return NotFound();

                    existing.Name = department.Name;
                    existing.Code = department.Code;
                    existing.Description = department.Description;
                    existing.FacultyID = department.FacultyID;
                    existing.IsActive = department.IsActive;
                    existing.UpdatedAt = DateTime.UtcNow;
                    existing.UpdatedBy = User.Identity?.Name ?? "system";

                    existing.DepartmentHeads.Clear();
                    existing.DepartmentHeads = _context.Staff
                        .Where(s => selectedStaff.Contains(s.StaffID))
                        .ToList();

                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentID))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name", department.FacultyID);
            ViewData["StaffList"] = new MultiSelectList(_context.Staff, "StaffID", "FullName", selectedStaff);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments
                .Include(d => d.Faculty)
                .FirstOrDefaultAsync(m => m.DepartmentID == id && !m.IsDeleted);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // POST: Departments/Delete/5 (soft delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department != null)
            {
                department.IsDeleted = true;
                department.IsActive = false;
                department.UpdatedAt = DateTime.UtcNow;
                department.UpdatedBy = User.Identity?.Name ?? "system";

                _context.Update(department);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentID == id && !e.IsDeleted);
        }
    }
}
