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
        private readonly ForgeDBContext _context;

        public StudyModesController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: StudyModes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudyModes.ToListAsync());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] StudyMode studyMode)
        {
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] StudyMode studyMode)
        {
            if (id != studyMode.Id)
            {
                return NotFound();
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
    }
}
