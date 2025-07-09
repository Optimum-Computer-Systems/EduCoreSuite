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
    public class StudyStatusesController : Controller
    {
        private readonly ForgeDBContext _context;

        public StudyStatusesController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: StudyStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudyStatuses.ToListAsync());
        }

        // GET: StudyStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyStatus = await _context.StudyStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyStatus == null)
            {
                return NotFound();
            }

            return View(studyStatus);
        }

        // GET: StudyStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyStatuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] StudyStatus studyStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studyStatus);
        }

        // GET: StudyStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyStatus = await _context.StudyStatuses.FindAsync(id);
            if (studyStatus == null)
            {
                return NotFound();
            }
            return View(studyStatus);
        }

        // POST: StudyStatuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] StudyStatus studyStatus)
        {
            if (id != studyStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyStatusExists(studyStatus.Id))
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
            return View(studyStatus);
        }

        // GET: StudyStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyStatus = await _context.StudyStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyStatus == null)
            {
                return NotFound();
            }

            return View(studyStatus);
        }

        // POST: StudyStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studyStatus = await _context.StudyStatuses.FindAsync(id);
            if (studyStatus != null)
            {
                _context.StudyStatuses.Remove(studyStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyStatusExists(int id)
        {
            return _context.StudyStatuses.Any(e => e.Id == id);
        }
    }
}
