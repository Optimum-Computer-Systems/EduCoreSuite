using System.Collections.Generic;
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

        public CoursesController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null) return NotFound();

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            var viewModel = new CourseFormViewModel
            {
                Course = new Course(),
                Departments = GetDepartments(),
                Programmes = GetProgrammes(),
                StudyLevels = GetStudyLevels(),
                Campuses = GetCampuses(),
                ExamBodies = GetExamBodies(),
                StudyStatuses = GetStudyStatuses()
            };

            return View(viewModel);
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(viewModel.Course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Re-populate dropdowns if validation fails
            viewModel.Departments = GetDepartments();
            viewModel.Programmes = GetProgrammes();
            viewModel.StudyLevels = GetStudyLevels();
            viewModel.Campuses = GetCampuses();
            viewModel.ExamBodies = GetExamBodies();
            viewModel.StudyStatuses = GetStudyStatuses();

            return View(viewModel);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            var viewModel = new CourseFormViewModel
            {
                Course = course,
                Departments = GetDepartments(),
                Programmes = GetProgrammes(),
                StudyLevels = GetStudyLevels(),
                Campuses = GetCampuses(),
                ExamBodies = GetExamBodies(),
                StudyStatuses = GetStudyStatuses()
            };

            return View(viewModel);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseFormViewModel viewModel)
        {
            if (id != viewModel.Course.CourseID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(viewModel.Course.CourseID))
                        return NotFound();
                    else
                        throw;
                }
            }

            viewModel.Departments = GetDepartments();
            viewModel.Programmes = GetProgrammes();
            viewModel.StudyLevels = GetStudyLevels();
            viewModel.Campuses = GetCampuses();
            viewModel.ExamBodies = GetExamBodies();
            viewModel.StudyStatuses = GetStudyStatuses();

            return View(viewModel);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null) _context.Courses.Remove(course);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }

        // Dropdown helpers
        private List<SelectListItem> GetDepartments() => new()
        {
            new("-- Select Department --", ""),
            new("Computer Science", "Computer Science"),
            new("Business", "Business"),
            new("Engineering", "Engineering"),
            new("Data Science", "Data Science")
        };

        private List<SelectListItem> GetProgrammes() => new()
        {
            new("-- Select Programme --", ""),
            new("Diploma", "Diploma"),
            new("Degree", "Degree"),
            new("Masters", "Masters")
        };

        private List<SelectListItem> GetStudyLevels() => new()
        {
            new("-- Select Study Level --", ""),
            new("All", "All"),
            new("1", "1"),
            new("2", "2"),
            new("3", "3"),
            new("4", "4")
        };

        private List<SelectListItem> GetCampuses() => new()
        {
            new("-- Select Campus --", ""),
            new("Main Campus", "Main Campus"),
            new("City Campus", "City Campus"),
            new("Online Campus", "Online Campus"),
            new("Downtown Campus", "Downtown Campus"),
            new("Tech Park", "Tech Park")
        };

        private List<SelectListItem> GetExamBodies() => new()
        {
            new("-- Select Exam Body --", ""),
            new("KNEC", "KNEC"),
            new("ICDL", "ICDL"),
            new("CISCO", "CISCO")
        };

        private List<SelectListItem> GetStudyStatuses() => new()
        {
            new("-- Select Study Status --", ""),
            new("Full Time", "Full Time"),
            new("Part Time", "Part Time")
        };
    }
}
