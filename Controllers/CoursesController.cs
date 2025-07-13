using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;
using EduCoreSuite.Models.ViewModels;

namespace EduCoreSuite.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ForgeDBContext _context;
        public CoursesController(ForgeDBContext context) => _context = context;

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Programme)
                .Include(c => c.Campus)
                .Include(c => c.ExamBody)
                .Include(c => c.StudyStatus)
                .Include(c => c.StudyMode)
                .ToListAsync();

            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Programme)
                .Include(c => c.Campus)
                .Include(c => c.ExamBody)
                .Include(c => c.StudyStatus)
                .Include(c => c.StudyMode)
                .FirstOrDefaultAsync(c => c.CourseID == id);

            return course == null ? NotFound() : View(course);
        }

        // GET: Create
        public IActionResult Create() => View(BuildFormViewModel(new Course()));

        // POST: Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(vm.Course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(BuildFormViewModel(vm.Course));
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses.FindAsync(id);
            return course == null ? NotFound() : View(BuildFormViewModel(course));
        }

        // POST: Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseFormViewModel vm)
        {
            if (id != vm.Course.CourseID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(vm.Course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(BuildFormViewModel(vm.Course));
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Programme)
                .Include(c => c.Campus)
                .Include(c => c.ExamBody)
                .Include(c => c.StudyStatus)
                .Include(c => c.StudyMode)
                .FirstOrDefaultAsync(c => c.CourseID == id);

            return course == null ? NotFound() : View(course);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null) _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private CourseFormViewModel BuildFormViewModel(Course course) =>
            new()
            {
                Course = course,
                Departments = _context.Departments
                    .Where(d => d.IsActive && !d.IsDeleted)
                    .OrderBy(d => d.Name)
                    .Select(d => new SelectListItem(d.Name, d.DepartmentID.ToString()))
                    .ToList(),
                Programmes = _context.Programmes
                    .OrderBy(p => p.Name)
                    .Select(p => new SelectListItem(p.Name, p.ProgrammeID.ToString()))
                    .ToList(),
                Campuses = _context.Campuses
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                    .ToList(),
                ExamBodies = _context.ExamBodies
                    .OrderBy(e => e.Name)
                    .Select(e => new SelectListItem(e.Name, e.Id.ToString()))
                    .ToList(),
                StudyStatuses = _context.StudyStatuses
                    .OrderBy(s => s.Name)
                    .Select(s => new SelectListItem(s.Name, s.Id.ToString()))
                    .ToList(),
                StudyModes = _context.StudyModes
                    .OrderBy(m => m.Name)
                    .Select(m => new SelectListItem(m.Name, m.Id.ToString()))
                    .ToList()
            };

        private bool CourseExists(int id) =>
            _context.Courses.Any(e => e.CourseID == id);
    }
}
