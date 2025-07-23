using EduCoreSuite.Data;
using EduCoreSuite.Models;
using EduCoreSuite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ForgeDBContext _context;
        private readonly ActivityService _activityService;

        public EnrollmentsController(ForgeDBContext context, ActivityService activityService)
        {
            _context = context;
            _activityService = activityService;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index(string searchTerm, string statusFilter, string courseFilter, string studentFilter, int page = 1, int pageSize = 20)
        {
            var enrollmentsQuery = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ThenInclude(c => c.Department)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                enrollmentsQuery = enrollmentsQuery.Where(e => 
                    e.Student.FullName.Contains(searchTerm) ||
                    e.Student.AdmissionNumber.Contains(searchTerm) ||
                    e.Course.CourseName.Contains(searchTerm) ||
                    e.Course.CourseCode.Contains(searchTerm));
            }

            // Apply status filter
            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                enrollmentsQuery = enrollmentsQuery.Where(e => e.Status == statusFilter);
            }

            // Apply course filter
            if (!string.IsNullOrWhiteSpace(courseFilter))
            {
                enrollmentsQuery = enrollmentsQuery.Where(e => e.Course.CourseName.Contains(courseFilter));
            }

            // Apply student filter
            if (!string.IsNullOrWhiteSpace(studentFilter))
            {
                enrollmentsQuery = enrollmentsQuery.Where(e => e.Student.FullName.Contains(studentFilter));
            }

            // Get total count for pagination
            var totalEnrollments = await enrollmentsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalEnrollments / (double)pageSize);

            // Apply pagination
            var enrollments = await enrollmentsQuery
                .OrderByDescending(e => e.EnrollmentDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Populate filter dropdowns
            ViewBag.StatusOptions = new SelectList(
                new[] { "Active", "Completed", "Withdrawn", "Suspended" },
                statusFilter);

            ViewBag.Courses = new SelectList(
                await _context.Courses.Select(c => c.CourseName).Distinct().OrderBy(c => c).ToListAsync(),
                courseFilter);

            ViewBag.Students = new SelectList(
                await _context.Students.Select(s => s.FullName).Distinct().OrderBy(s => s).ToListAsync(),
                studentFilter);

            // Pagination and filter data
            ViewBag.SearchTerm = searchTerm;
            ViewBag.StatusFilter = statusFilter;
            ViewBag.CourseFilter = courseFilter;
            ViewBag.StudentFilter = studentFilter;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalEnrollments = totalEnrollments;
            ViewBag.PageSize = pageSize;

            return View(enrollments);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ThenInclude(c => c.Department)
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            return enrollment == null ? NotFound() : View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Enrollments/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            // Check for duplicate enrollment
            if (await _context.Enrollments.AnyAsync(e => 
                e.StudentID == enrollment.StudentID && 
                e.CourseID == enrollment.CourseID && 
                e.Status == "Active"))
            {
                ModelState.AddModelError(string.Empty, "Student is already enrolled in this course.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();

                // Log the activity
                var student = await _context.Students.FindAsync(enrollment.StudentID);
                var course = await _context.Courses.FindAsync(enrollment.CourseID);
                
                await _activityService.LogSystemActivity(
                    "New Enrollment", 
                    $"{student?.FullName} enrolled in {course?.CourseName}");

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns();
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            PopulateDropdowns();
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();

                    // Log the activity
                    var student = await _context.Students.FindAsync(enrollment.StudentID);
                    var course = await _context.Courses.FindAsync(enrollment.CourseID);
                    
                    await _activityService.LogSystemActivity(
                        "Enrollment Updated", 
                        $"Enrollment updated for {student?.FullName} in {course?.CourseName}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Enrollments.AnyAsync(e => e.EnrollmentID == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns();
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            return enrollment == null ? NotFound() : View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);
                
            if (enrollment != null)
            {
                string studentName = enrollment.Student?.FullName ?? "Unknown";
                string courseName = enrollment.Course?.CourseName ?? "Unknown";
                
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();

                // Log the activity
                await _activityService.LogSystemActivity(
                    "Enrollment Removed", 
                    $"Enrollment removed for {studentName} from {courseName}");
            }

            return RedirectToAction(nameof(Index));
        }

        // Bulk Enrollment
        public IActionResult BulkEnroll()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkEnroll(int[] studentIds, int[] courseIds, string academicYear, string semester)
        {
            if (studentIds == null || courseIds == null || !studentIds.Any() || !courseIds.Any())
            {
                ModelState.AddModelError(string.Empty, "Please select at least one student and one course.");
                PopulateDropdowns();
                return View();
            }

            var enrollmentsToAdd = new List<Enrollment>();
            var duplicateCount = 0;

            foreach (var studentId in studentIds)
            {
                foreach (var courseId in courseIds)
                {
                    // Check if enrollment already exists
                    if (!await _context.Enrollments.AnyAsync(e => 
                        e.StudentID == studentId && 
                        e.CourseID == courseId && 
                        e.Status == "Active"))
                    {
                        enrollmentsToAdd.Add(new Enrollment
                        {
                            StudentID = studentId,
                            CourseID = courseId,
                            EnrollmentDate = DateTime.Now,
                            Status = "Active",
                            AcademicYear = academicYear ?? DateTime.Now.Year.ToString(),
                            Semester = semester ?? "1"
                        });
                    }
                    else
                    {
                        duplicateCount++;
                    }
                }
            }

            if (enrollmentsToAdd.Any())
            {
                _context.Enrollments.AddRange(enrollmentsToAdd);
                await _context.SaveChangesAsync();

                // Log the activity
                await _activityService.LogSystemActivity(
                    "Bulk Enrollment", 
                    $"Bulk enrolled {enrollmentsToAdd.Count} students in courses. {duplicateCount} duplicates skipped.");

                TempData["SuccessMessage"] = $"Successfully enrolled {enrollmentsToAdd.Count} students. {duplicateCount} duplicates were skipped.";
            }
            else
            {
                TempData["ErrorMessage"] = "No new enrollments were created. All selected combinations already exist.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns()
        {
            ViewBag.Students = new SelectList(
                _context.Students.OrderBy(s => s.FullName).ToList(),
                nameof(Student.StudentID),
                nameof(Student.FullName));

            ViewBag.Courses = new SelectList(
                _context.Courses.Include(c => c.Department).OrderBy(c => c.CourseName).ToList(),
                nameof(Course.CourseID),
                nameof(Course.CourseName));

            ViewBag.StatusOptions = new SelectList(
                new[] { "Active", "Completed", "Withdrawn", "Suspended" });
        }
    }
}