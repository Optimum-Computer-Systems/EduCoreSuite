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

// StudentsController.cs  ── only the helper has changed
private void PopulateDropdowns()
{
    // --- simple look‑ups (no tables) ------------------------------
    ViewBag.GenderList       = new SelectList(new[] { "Male", "Female", "Other" });
    ViewBag.Religions        = new SelectList(new[] { "Christianity", "Islam", "Hinduism", "Atheist", "Other" });
    ViewBag.Medicals         = new SelectList(new[] { "Normal", "Chronic", "Disabled", "Other" });
    ViewBag.MaritalStatusList= new SelectList(new[] { "Single", "Married", "Divorced", "Widowed" });
    ViewBag.Years            = new SelectList(new[] { "1st Year", "2nd Year", "3rd Year", "4th Year" });

    // --- database‑driven lists ------------------------------------
    // NB:  ID field first (model‑binding)  |  Display field second
    ViewBag.Courses      = new SelectList(
        _context.Courses?.AsNoTracking().ToList() ?? new List<Course>(),
        nameof(Course.CourseID), nameof(Course.CourseName));

    ViewBag.Departments  = new SelectList(
        _context.Departments?.AsNoTracking().ToList() ?? new List<Department>(),
        nameof(Department.DepartmentID), nameof(Department.Name));

    ViewBag.Faculties    = new SelectList(
        _context.Faculties?.AsNoTracking().ToList() ?? new List<Faculty>(),
        nameof(Faculty.FacultyID), nameof(Faculty.Name));

    ViewBag.ExamBodies   = new SelectList(
        _context.ExamBodies?.AsNoTracking().ToList() ?? new List<ExamBody>(),
        nameof(ExamBody.Id), nameof(ExamBody.Name));

    // ――――― NEW items from the navigation snapshot ―――――
    ViewBag.Campuses     = new SelectList(
        _context.Campuses?.AsNoTracking().ToList() ?? new List<Campus>(),
        nameof(Campus.Id), nameof(Campus.Name));

    ViewBag.Programmes   = new SelectList(
        _context.Programmes?.AsNoTracking().ToList() ?? new List<Programme>(),
        nameof(Programme.ProgrammeID), nameof(Programme.Name));

    ViewBag.Staff        = new SelectList(
        _context.Staff?.AsNoTracking().ToList() ?? new List<Staff>(),
        nameof(Staff.StaffID), nameof(Staff.FullName));

    ViewBag.StudyModes   = new SelectList(
        _context.StudyModes?.AsNoTracking().ToList() ?? new List<StudyMode>(),
        nameof(StudyMode.Id), nameof(StudyMode.Name));

    ViewBag.StudyStatuses= new SelectList(
        _context.StudyStatuses?.AsNoTracking().ToList() ?? new List<StudyStatus>(),
        nameof(StudyStatus.Id), nameof(StudyStatus.Name));

            ViewBag.Counties = new SelectList(
                _context.Counties              // DbSet<CountySubCounty>
                        .AsNoTracking()
                        .OrderBy(c => c.CountyName)
                        .ToList(),
                nameof(CountySubCounty.CountyID),     // value field
                nameof(CountySubCounty.CountyName));  // text  field

            ViewBag.SubCounties = new SelectList(
                Enumerable.Empty<SubCounty>(),         // blank on first load
                nameof(SubCounty.SubCountyID),
                nameof(SubCounty.SubCountyName));

        }


        [HttpGet]
        public JsonResult GetSubCounties(int countyId)
        {
            var data = _context.SubCounties        // DbSet<SubCounty>
                               .AsNoTracking()
                               .Where(s => s.CountyID == countyId)
                               .OrderBy(s => s.SubCountyName)
                               .Select(s => new
                               {
                                   s.SubCountyID,
                                   s.SubCountyName
                               })
                               .ToList();

            return Json(data);
        }

    }
}
