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
    public class StudyModesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudyModesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudyModes
        public async Task<IActionResult> Index(string searchTerm)
        {
            var studyModesQuery = _context.StudyModes.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                studyModesQuery = studyModesQuery.Where(sm => 
                    sm.Name.Contains(searchTerm) ||
                    (sm.Description != null && sm.Description.Contains(searchTerm)));
            }

            var studyModes = await studyModesQuery.OrderBy(sm => sm.Name).ToListAsync();
            
            // Pass search term to view
            ViewBag.SearchTerm = searchTerm;
            
            return View(studyModes);
        }

        // GET: StudyModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyMode = await _context.StudyModes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyMode == null)
            {
                return NotFound();
            }

            return View(studyMode);
        }

        // GET: StudyModes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyModes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] StudyMode studyMode)
        {
            // Check for duplicate StudyMode Name
            if (await _context.StudyModes.AnyAsync(s => s.Name.ToLower() == studyMode.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A study mode with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(studyMode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studyMode);
        }

        // GET: StudyModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyMode = await _context.StudyModes.FindAsync(id);
            if (studyMode == null)
            {
                return NotFound();
            }
            return View(studyMode);
        }

        // POST: StudyModes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] StudyMode studyMode)
        {
            if (id != studyMode.Id)
            {
                return NotFound();
            }

            // Check for duplicate StudyMode Name (excluding current study mode)
            if (await _context.StudyModes.AnyAsync(s => s.Id != id && s.Name.ToLower() == studyMode.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A study mode with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyModeExists(studyMode.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studyMode);
        }

        // GET: StudyModes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyMode = await _context.StudyModes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyMode == null)
            {
                return NotFound();
            }

            return View(studyMode);
        }

        // POST: StudyModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studyMode = await _context.StudyModes.FindAsync(id);
            if (studyMode != null)
            {
                _context.StudyModes.Remove(studyMode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyModeExists(int id)
        {
            return _context.StudyModes.Any(e => e.Id == id);
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
                isDuplicate = await _context.StudyModes.AnyAsync(s => s.Id != excludeId && s.Name.ToLower() == name.ToLower());
            }
            else
            {
                isDuplicate = await _context.StudyModes.AnyAsync(s => s.Name.ToLower() == name.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A study mode with this name already exists." : "" });
        }
    }
}
