using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using NuGet.Protocol.Plugins;

namespace EduCoreSuite.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ForgeDBContext _context;
        private object f;

        public StudentsController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns();
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            PopulateDropdowns();
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns();
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }

        private void PopulateDropdowns()
        {
            ViewBag.GenderList = new SelectList(new[] { "Male", "Female", "Other" });
            ViewBag.Religions = new SelectList(new[] { "Christianity", "Islam", "Hinduism", "Atheist", "Other" });
            ViewBag.Medicals = new SelectList(new[] { "Normal", "Chronic", "Disabled", "Other" });
            ViewBag.MaritalStatusList = new SelectList(new[] { "Single", "Married", "Divorced", "Widowed" });

            ViewBag.Courses = new SelectList(_context.Courses?.ToList() ?? new List<Course>(), "CourseName", "CourseName");
            ViewBag.Departments = new SelectList(_context.Departments?.ToList() ?? new List<Department>(), "DepartmentName", "DepartmentName");
            ViewBag.Faculties = new SelectList(_context.Faculties?.ToList() ?? new List<Faculty>(), "FacultyName", "FacultyName");
            ViewBag.ExamBodies = new SelectList(_context.ExamBodies?.ToList() ?? new List<ExamBody>(), "BodyName", "BodyName");
            ViewBag.Programs = new SelectList(new[] { "Certificate", "Diploma", "Degree", "Masters" });
            ViewBag.Years = new SelectList(new[] { "1st Year", "2nd Year", "3rd Year", "4th Year" });

            ViewBag.Counties = new SelectList(CountySubCountyData.CountySubCountyDict.Keys);
            ViewBag.SubCounties = new SelectList(
                CountySubCountyData.CountySubCountyDict.SelectMany(kvp => kvp.Value).Distinct().OrderBy(x => x).ToList()
            );
        }

        // Optional JSON method if needed in future
        [HttpGet]
        public JsonResult GetSubCounties(string county)
        {
            var subCounties = CountySubCountyData.CountySubCountyDict
                .FirstOrDefault(c => c.Key.Equals(county, System.StringComparison.OrdinalIgnoreCase)).Value
                ?? new List<string>();

            return Json(subCounties);
        }
    }
}
