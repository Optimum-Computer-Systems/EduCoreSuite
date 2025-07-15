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
    public class ExamBodiesController : Controller
    {
        private readonly ForgeDBContext _context;

        public ExamBodiesController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: ExamBodies
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExamBodies.ToListAsync());
        }

        // GET: ExamBodies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examBody = await _context.ExamBodies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examBody == null)
            {
                return NotFound();
            }

            return View(examBody);
        }

        // GET: ExamBodies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamBodies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Country")] ExamBody examBody)
        {
            // Check for duplicate ExamBody Name
            if (await _context.ExamBodies.AnyAsync(e => e.Name.ToLower() == examBody.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "An exam body with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(examBody);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(examBody);
        }

        // GET: ExamBodies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examBody = await _context.ExamBodies.FindAsync(id);
            if (examBody == null)
            {
                return NotFound();
            }
            return View(examBody);
        }

        // POST: ExamBodies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Country")] ExamBody examBody)
        {
            if (id != examBody.Id)
            {
                return NotFound();
            }

            // Check for duplicate ExamBody Name (excluding current exam body)
            if (await _context.ExamBodies.AnyAsync(e => e.Id != id && e.Name.ToLower() == examBody.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "An exam body with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examBody);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamBodyExists(examBody.Id))
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
            return View(examBody);
        }

        // GET: ExamBodies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examBody = await _context.ExamBodies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examBody == null)
            {
                return NotFound();
            }

            return View(examBody);
        }

        // POST: ExamBodies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examBody = await _context.ExamBodies.FindAsync(id);
            if (examBody != null)
            {
                _context.ExamBodies.Remove(examBody);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamBodyExists(int id)
        {
            return _context.ExamBodies.Any(e => e.Id == id);
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
                isDuplicate = await _context.ExamBodies.AnyAsync(e => e.Id != excludeId && e.Name.ToLower() == name.ToLower());
            }
            else
            {
                isDuplicate = await _context.ExamBodies.AnyAsync(e => e.Name.ToLower() == name.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "An exam body with this name already exists." : "" });
        }
    }
}
