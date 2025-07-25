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
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index(string searchTerm, string facultyFilter, bool? statusFilter, int page = 1, int pageSize = 20)
        {
            var departmentsQuery = _context.Departments
                .Include(d => d.Faculty)
                .Include(d => d.DepartmentHeads)
                .Where(d => !d.IsDeleted)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                departmentsQuery = departmentsQuery.Where(d => 
                    d.Name.Contains(searchTerm) ||
                    d.Code.Contains(searchTerm) ||
                    (d.Description != null && d.Description.Contains(searchTerm)) ||
                    d.Faculty.Name.Contains(searchTerm));
            }

            // Apply faculty filter
            if (!string.IsNullOrWhiteSpace(facultyFilter))
            {
                departmentsQuery = departmentsQuery.Where(d => d.Faculty.Name == facultyFilter);
            }

            // Apply status filter
            if (statusFilter.HasValue)
            {
                departmentsQuery = departmentsQuery.Where(d => d.IsActive == statusFilter.Value);
            }

            // Get total count for pagination
            var totalDepartments = await departmentsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalDepartments / (double)pageSize);

            // Apply pagination
            var departments = await departmentsQuery
                .OrderBy(d => d.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Populate filter dropdowns
            ViewBag.Faculties = new SelectList(
                await _context.Faculties.Select(f => f.Name).Distinct().OrderBy(f => f).ToListAsync(),
                facultyFilter);

            // Pagination and filter data
            ViewBag.SearchTerm = searchTerm;
            ViewBag.FacultyFilter = facultyFilter;
            ViewBag.StatusFilter = statusFilter;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalDepartments = totalDepartments;
            ViewBag.PageSize = pageSize;

            return View(departments);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                .Include(d => d.Faculty)
                .Include(d => d.DepartmentHeads)
                .FirstOrDefaultAsync(m => m.DepartmentID == id && !m.IsDeleted);

            if (department == null) return NotFound();

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            PopulateSelections();
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department, int[] selectedStaff)
        {
            selectedStaff ??= Array.Empty<int>();

            // Check for duplicate Department Name
            if (await _context.Departments.AnyAsync(d => !d.IsDeleted && d.Name.ToLower() == department.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A department with this name already exists.");
            }

            // Check for duplicate Department Code
            if (await _context.Departments.AnyAsync(d => !d.IsDeleted && d.Code.ToLower() == department.Code.ToLower()))
            {
                ModelState.AddModelError("Code", "A department with this code already exists.");
            }

            if (ModelState.IsValid)
            {
                department.IsActive = true;
                department.IsDeleted = false;
                department.DepartmentHeads = _context.Staff
                    .Where(s => selectedStaff.Contains(s.StaffID))
                    .ToList();

                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateSelections(department.FacultyID, selectedStaff);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                .Include(d => d.DepartmentHeads)
                .FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (department == null) return NotFound();

            PopulateSelections(department.FacultyID, department.DepartmentHeads.Select(s => s.StaffID).ToArray());
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department, int[] selectedStaff)
        {
            if (id != department.DepartmentID) return NotFound();

            selectedStaff ??= Array.Empty<int>();

            // Check for duplicate Department Name (excluding current department)
            if (await _context.Departments.AnyAsync(d => !d.IsDeleted && d.DepartmentID != id && d.Name.ToLower() == department.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A department with this name already exists.");
            }

            // Check for duplicate Department Code (excluding current department)
            if (await _context.Departments.AnyAsync(d => !d.IsDeleted && d.DepartmentID != id && d.Code.ToLower() == department.Code.ToLower()))
            {
                ModelState.AddModelError("Code", "A department with this code already exists.");
            }

            if (ModelState.IsValid)
            {
                var existing = await _context.Departments
                    .Include(d => d.DepartmentHeads)
                    .FirstOrDefaultAsync(d => d.DepartmentID == id);

                if (existing == null) return NotFound();

                existing.Name = department.Name;
                existing.Code = department.Code;
                existing.Description = department.Description;
                existing.FacultyID = department.FacultyID;
                existing.IsActive = department.IsActive;
                existing.DepartmentHeads = _context.Staff
                    .Where(s => selectedStaff.Contains(s.StaffID))
                    .ToList();

                _context.Update(existing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateSelections(department.FacultyID, selectedStaff);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                .Include(d => d.Faculty)
                .FirstOrDefaultAsync(m => m.DepartmentID == id && !m.IsDeleted);

            if (department == null) return NotFound();

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
                _context.Update(department);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id) =>
            _context.Departments.Any(e => e.DepartmentID == id && !e.IsDeleted);

        private void PopulateSelections(int? facultyId = null, int[]? staffIds = null)
        {
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "Name", facultyId);
            ViewData["StaffList"] = new MultiSelectList(_context.Staff, "StaffID", "FullName", staffIds);
        }

        

        // AJAX endpoints for real-time duplicate checking
        [HttpGet]
        public async Task<IActionResult> CheckDuplicateName(string name, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Departments.AnyAsync(d => !d.IsDeleted && d.DepartmentID != excludeId && d.Name.ToLower() == name.ToLower());
            }
            else
            {
                isDuplicate = await _context.Departments.AnyAsync(d => !d.IsDeleted && d.Name.ToLower() == name.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A department with this name already exists." : "" });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicateCode(string code, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Departments.AnyAsync(d => !d.IsDeleted && d.DepartmentID != excludeId && d.Code.ToLower() == code.ToLower());
            }
            else
            {
                isDuplicate = await _context.Departments.AnyAsync(d => !d.IsDeleted && d.Code.ToLower() == code.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A department with this code already exists." : "" });
        }
    }
}
