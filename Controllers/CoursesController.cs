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
        public async Task<IActionResult> Index(string departmentSearch = "", string courseSearch = "", int page = 1)
        {
            // Set page size - number of courses per page
            int pageSize = 5;
            
            // Start with all courses
            var coursesQuery = _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Programme)
                .Include(c => c.Campus)
                .Include(c => c.ExamBody)
                .Include(c => c.StudyStatus)
                .Include(c => c.StudyMode)
                .AsQueryable();
                
            // Apply department search filter if provided
            if (!string.IsNullOrEmpty(departmentSearch))
            {
                string searchTerm = departmentSearch.ToLower();
                coursesQuery = coursesQuery.Where(c => c.Department != null && 
                    c.Department.Name.ToLower().Contains(searchTerm));
            }
            
            // Apply course name search filter if provided
            if (!string.IsNullOrEmpty(courseSearch))
            {
                string searchTerm = courseSearch.ToLower();
                coursesQuery = coursesQuery.Where(c => c.CourseName.ToLower().Contains(searchTerm));
            }
            
            // Get total courses count for pagination
            var totalCourses = await coursesQuery.CountAsync();
            
            // Calculate total pages
            int totalPages = (int)Math.Ceiling(totalCourses / (double)pageSize);
            
            // Ensure page is within valid range
            page = Math.Max(1, Math.Min(page, Math.Max(1, totalPages)));
            
            // Get paginated courses
            var paginatedCourses = await coursesQuery
                .OrderBy(c => c.Department.Name)
                .ThenBy(c => c.CourseName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            // Group courses by department
            var coursesByDepartment = paginatedCourses
                .GroupBy(c => c.Department)
                .OrderBy(g => g.Key.Name)
                .ToDictionary(g => g.Key, g => g.ToList());
            
            // Create view model
            var viewModel = new CourseIndexViewModel
            {
                CoursesByDepartment = coursesByDepartment,
                DepartmentSearch = departmentSearch,
                CourseSearch = courseSearch,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };
            
            return View(viewModel);
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
            // Check for duplicate Course Name
            if (await _context.Courses.AnyAsync(c => c.CourseName.ToLower() == vm.Course.CourseName.ToLower()))
            {
                ModelState.AddModelError("Course.CourseName", "A course with this name already exists.");
            }

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

            // Check for duplicate Course Name (excluding current course)
            if (await _context.Courses.AnyAsync(c => c.CourseID != id && c.CourseName.ToLower() == vm.Course.CourseName.ToLower()))
            {
                ModelState.AddModelError("Course.CourseName", "A course with this name already exists.");
            }

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

        // AJAX endpoint for real-time duplicate checking
        [HttpGet]
        public async Task<IActionResult> CheckDuplicateCourseName(string courseName, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Courses.AnyAsync(c => c.CourseID != excludeId && c.CourseName.ToLower() == courseName.ToLower());
            }
            else
            {
                isDuplicate = await _context.Courses.AnyAsync(c => c.CourseName.ToLower() == courseName.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A course with this name already exists." : "" });
        }
        
        // AJAX endpoint to get programme level
        [HttpGet]
        public async Task<IActionResult> GetProgrammeLevel(int programmeId)
        {
            var programme = await _context.Programmes.FindAsync(programmeId);
            if (programme == null)
                return NotFound();
                
            return Json(new { level = programme.Level.ToString() });
        }


    }
}
