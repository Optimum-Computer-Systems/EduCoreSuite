using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ForgeDBContext _context;

        public StudentsController(ForgeDBContext context)
        {
            _context = context;
        }

        // ------------------------- INDEX & DETAILS -------------------------

        public async Task<IActionResult> Index(string searchTerm, string departmentFilter, string courseFilter, string programmeFilter, string examBodyFilter, string yearFilter, int page = 1, int pageSize = 20)
        {
            var studentsQuery = _context.Students.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                studentsQuery = studentsQuery.Where(s => 
                    s.FullName.Contains(searchTerm) ||
                    s.AdmissionNumber.Contains(searchTerm) ||
                    s.Email.Contains(searchTerm) ||
                    s.IDNumber.Contains(searchTerm));
            }

            // Apply department filter
            if (!string.IsNullOrWhiteSpace(departmentFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Department == departmentFilter);
            }

            // Apply course filter
            if (!string.IsNullOrWhiteSpace(courseFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Course == courseFilter);
            }

            // Apply programme filter
            if (!string.IsNullOrWhiteSpace(programmeFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Program == programmeFilter);
            }

            // Apply exam body filter
            if (!string.IsNullOrWhiteSpace(examBodyFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.ExamBody == examBodyFilter);
            }

            // Apply year filter
            if (!string.IsNullOrWhiteSpace(yearFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Year == yearFilter);
            }



            // Get total count for pagination
            var totalStudents = await studentsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalStudents / (double)pageSize);

            // Apply pagination
            var students = await studentsQuery
                .OrderBy(s => s.FullName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Populate filter dropdowns
            ViewBag.Departments = new SelectList(
                await _context.Students.Select(s => s.Department).Distinct().OrderBy(d => d).ToListAsync(),
                departmentFilter);

            ViewBag.Courses = new SelectList(
                await _context.Students.Select(s => s.Course).Distinct().OrderBy(c => c).ToListAsync(),
                courseFilter);

            ViewBag.Programmes = new SelectList(
                new[] { "Certificate", "Diploma", "Degree", "Masters" },
                programmeFilter);

            ViewBag.ExamBodies = new SelectList(
                await _context.Students.Select(s => s.ExamBody).Distinct().OrderBy(e => e).ToListAsync(),
                examBodyFilter);

            ViewBag.Years = new SelectList(
                new[] { "1st Year", "2nd Year", "3rd Year", "4th Year" },
                yearFilter);



            // Pagination and filter data
            ViewBag.SearchTerm = searchTerm;
            ViewBag.DepartmentFilter = departmentFilter;
            ViewBag.CourseFilter = courseFilter;
            ViewBag.ProgrammeFilter = programmeFilter;
            ViewBag.ExamBodyFilter = examBodyFilter;
            ViewBag.YearFilter = yearFilter;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalStudents = totalStudents;
            ViewBag.PageSize = pageSize;

            return View(students);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.AsNoTracking()
                                                 .FirstOrDefaultAsync(s => s.StudentID == id);
            return student == null ? NotFound() : View(student);
        }

        // ------------------------- CREATE -------------------------

        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            // Duplicate check
            if (await IsDuplicateAsync(student))
            {
                ModelState.AddModelError(string.Empty,
                    "A student with the same Admission Number, Email, or National ID already exists.");
            }

            // Validate SubCounty
            if (!_context.SubCounties.Any(s => s.SubCountyID == student.SubCountyID && s.CountyID == student.CountyID))
            {
                ModelState.AddModelError("SubCountyID", "Invalid sub-county selection.");
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(student.CountyID);
                return View(student);
            }

            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ------------------------- EDIT -------------------------

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            PopulateDropdowns(student.CountyID);
            return View(student);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentID) return NotFound();

            if (await IsDuplicateAsync(student, id))
            {
                ModelState.AddModelError(string.Empty,
                    "Another student already uses this Admission Number, Email, or National ID.");
            }

            // Validate SubCounty
            if (!_context.SubCounties.Any(s => s.SubCountyID == student.SubCountyID && s.CountyID == student.CountyID))
            {
                ModelState.AddModelError("SubCountyID", "Invalid sub-county selection.");
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(student.CountyID);
                return View(student);
            }

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Students.AnyAsync(e => e.StudentID == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // ------------------------- DELETE -------------------------

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.AsNoTracking()
                                                 .FirstOrDefaultAsync(s => s.StudentID == id);
            return student == null ? NotFound() : View(student);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
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

        // ------------------------- DUPLICATE CHECK -------------------------

        private async Task<bool> IsDuplicateAsync(Student s, int excludeId = 0)
        {
            return await _context.Students.AsNoTracking()
                .AnyAsync(x =>
                       x.StudentID != excludeId &&
                      (x.AdmissionNumber == s.AdmissionNumber ||
                       x.Email == s.Email ||
                       x.IDNumber == s.IDNumber));
        }

        // ------------------------- DROPDOWNS -------------------------

        private void PopulateDropdowns(int countyId = 0)
        {
            ViewBag.Courses = BuildSelectList(
                _context.Courses, c => c.CourseName, "-- Select Course --");

            ViewBag.Departments = BuildSelectList(
                _context.Departments, d => d.Name, "-- Select Department --");

            ViewBag.Faculties = BuildSelectList(
                _context.Faculties, f => f.Name, "-- Select Faculty --");

            ViewBag.ExamBodies = BuildSelectList(
                _context.ExamBodies, e => e.Name, "-- Select Exam Body --");

            ViewBag.Counties = new SelectList(
                _context.Counties.AsNoTracking()
                                 .OrderBy(c => c.CountyName)
                                 .ToList(),
                nameof(CountySubCounty.CountyID),
                nameof(CountySubCounty.CountyName),
                countyId);

            ViewBag.SubCounties = new SelectList(
                countyId == 0
                    ? Enumerable.Empty<SubCounty>()
                    : _context.SubCounties
                              .Where(sc => sc.CountyID == countyId)
                              .OrderBy(sc => sc.SubCountyName)
                              .ToList(),
                nameof(SubCounty.SubCountyID),
                nameof(SubCounty.SubCountyName));
        }

        private static IEnumerable<SelectListItem> BuildSelectList<T>(
            IQueryable<T> query,
            Func<T, string> selector,
            string placeholder) where T : class
        {
            var list = query.AsNoTracking()
                            .Select(item => new SelectListItem
                            {
                                Value = selector(item),
                                Text = selector(item)
                            })
                            .ToList();

            list.Insert(0, new SelectListItem
            {
                Value = "",
                Text = placeholder
            });

            return list;
        }

        // ------------------------- AJAX: SUBCOUNTIES -------------------------

        [HttpGet]
        public JsonResult GetSubCounties(int countyId)
        {
            var data = _context.SubCounties.AsNoTracking()
                               .Where(s => s.CountyID == countyId)
                               .OrderBy(s => s.SubCountyName)
                               .Select(s => new { s.SubCountyID, s.SubCountyName })
                               .ToList();

            return Json(data);
        }
    }
}