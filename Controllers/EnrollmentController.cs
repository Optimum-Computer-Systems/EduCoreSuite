using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;
using EduCoreSuite.Models.ViewModels;
using EduCoreSuite.Models.EnrollmentModes;

namespace EduCoreSuite.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ForgeDBContext _context;

        public EnrollmentController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Enrollment Form
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Select(s => new SelectListItem
                {
                    Text = s.FullName,
                    Value = s.StudentID.ToString()
                }).ToListAsync();

            var courses = await _context.Courses
                .Select(c => new SelectListItem
                {
                    Text = c.CourseName,
                    Value = c.CourseID.ToString()
                }).ToListAsync();

            var vm = new CourseEnrollmentViewModel
            {
                Students = students,
                Courses = courses,
                Campuses = new[] { "Main Campus", "City Campus", "Online Campus", "Downtown Campus", "Tech Park" }
                    .Select(c => new SelectListItem { Text = c, Value = c }).ToList(),
                Years = new[] { "1", "2", "3", "4" }
                    .Select(y => new SelectListItem { Text = y, Value = y }).ToList()
            };

            vm.Students.Insert(0, new SelectListItem { Text = "-- Select Student --", Value = "0" });
            vm.Courses.Insert(0, new SelectListItem { Text = "-- Select Course --", Value = "0" });
            vm.Campuses.Insert(0, new SelectListItem { Text = "-- Select Campus --", Value = "" });
            vm.Years.Insert(0, new SelectListItem { Text = "-- Select Year --", Value = "" });

            ViewBag.AllCourses = await _context.Courses
                .Select(c => new { c.CourseID, c.Department })
                .ToListAsync();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartment(int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseID == courseId);
            return Content(course?.Department ?? "");
        }

        [HttpPost]
        public async Task<IActionResult> EnrollSingle(CourseEnrollmentViewModel m)
        {
            if (m.SelectedStudentId <= 0 || m.SelectedCourseId <= 0 ||
                string.IsNullOrWhiteSpace(m.SelectedDepartment) ||
                string.IsNullOrWhiteSpace(m.SelectedCampus) ||
                string.IsNullOrWhiteSpace(m.SelectedYear))
            {
                return Error("Please fill all required fields: student, course, department, campus, and year.");
            }

            if (await IsAlreadyEnrolled(m.SelectedStudentId, m.SelectedCourseId))
                return Error("This student is already enrolled in this course");

            await AddEnrollment_SingleStudent_SingleCourse(m.SelectedStudentId, m.SelectedCourseId, m);
            return Success("Student enrolled successfully!");
        }

        [HttpPost]
        public async Task<IActionResult> EnrollMultipleStudentsSingleCourse(CourseEnrollmentViewModel m)
        {
            if (m.SelectedStudentIds == null || !m.SelectedStudentIds.Any() ||
                m.SelectedCourseId <= 0 ||
                string.IsNullOrWhiteSpace(m.SelectedDepartment) ||
                string.IsNullOrWhiteSpace(m.SelectedCampus) ||
                string.IsNullOrWhiteSpace(m.SelectedYear))
            {
                return Error("Please fill all required fields: students, course, department, campus, and year.");
            }

            var added = 0;
            foreach (var studentId in m.SelectedStudentIds.Distinct().Where(id => id > 0))
            {
                if (!await IsAlreadyEnrolled(studentId, m.SelectedCourseId))
                {
                    await AddEnrollment_MultipleStudents_SingleCourse(studentId, m.SelectedCourseId, m);
                    added++;
                }
            }

            return added == 0
                ? Error("All selected students are already enrolled in the course.")
                : Success($"Enrolled {added} student(s) successfully!");
        }

        [HttpPost]
        public async Task<IActionResult> EnrollSingleStudentMultipleCourses(CourseEnrollmentViewModel m)
        {
            if (m.SelectedStudentId <= 0 ||
                m.SelectedCourseIds == null || !m.SelectedCourseIds.Any() ||
                string.IsNullOrWhiteSpace(m.SelectedDepartment) ||
                string.IsNullOrWhiteSpace(m.SelectedCampus) ||
                string.IsNullOrWhiteSpace(m.SelectedYear))
            {
                return Error("Please fill all required fields: student, courses, department, campus, and year.");
            }

            var added = 0;
            foreach (var courseId in m.SelectedCourseIds.Distinct().Where(id => id > 0))
            {
                if (!await IsAlreadyEnrolled(m.SelectedStudentId, courseId))
                {
                    await AddEnrollment_SingleStudent_MultipleCourses(m.SelectedStudentId, courseId, m);
                    added++;
                }
            }

            return added == 0
                ? Error("This student is already enrolled in all the selected courses.")
                : Success($"Enrolled in {added} course(s) successfully!");
        }

        [HttpPost]
        public async Task<IActionResult> EnrollMultipleStudentsMultipleCourses(CourseEnrollmentViewModel m)
        {
            if (m.SelectedStudentIds == null || !m.SelectedStudentIds.Any() ||
                m.SelectedCourseIds == null || !m.SelectedCourseIds.Any() ||
                string.IsNullOrWhiteSpace(m.SelectedDepartment) ||
                string.IsNullOrWhiteSpace(m.SelectedCampus) ||
                string.IsNullOrWhiteSpace(m.SelectedYear))
            {
                return Error("Please fill all required fields: students, courses, department, campus, and year.");
            }

            var count = 0;
            foreach (var sid in m.SelectedStudentIds.Distinct().Where(id => id > 0))
            {
                foreach (var cid in m.SelectedCourseIds.Distinct().Where(id => id > 0))
                {
                    if (!await IsAlreadyEnrolled(sid, cid))
                    {
                        await AddEnrollment_MultipleStudents_MultipleCourses(sid, cid, m);
                        count++;
                    }
                }
            }

            return count == 0
                ? Error("All students are already enrolled in all selected courses.")
                : Success($"Created {count} new enrollment(s) successfully!");
        }

        private async Task AddEnrollment_SingleStudent_SingleCourse(int studentId, int courseId, CourseEnrollmentViewModel m)
        {
            var student = await _context.Students.FindAsync(studentId);
            var course = await _context.Courses.FindAsync(courseId);

            if (student == null || course == null)
                return;

            _context.Enrollment_SingleStudent_SingleCourse.Add(new Enrollment_SingleStudent_SingleCourse
            {
                StudentID = studentId,
                StudentName = student.FullName ?? "Unknown",
                CourseID = courseId,
                CourseName = course.CourseName ?? "Unknown",
                Department = course.Department ?? "Unknown",
                Campus = m.SelectedCampus ?? "Unknown",
                Year = m.SelectedYear ?? "Unknown"
            });

            await _context.SaveChangesAsync();
        }

        private async Task AddEnrollment_MultipleStudents_SingleCourse(int studentId, int courseId, CourseEnrollmentViewModel m)
        {
            var student = await _context.Students.FindAsync(studentId);
            var course = await _context.Courses.FindAsync(courseId);

            if (student == null || course == null)
                return;

            _context.Enrollment_MultipleStudents_SingleCourse.Add(new Enrollment_MultipleStudents_SingleCourse
            {
                StudentID = studentId,
                StudentName = student.FullName ?? "Unknown",
                CourseID = courseId,
                CourseName = course.CourseName ?? "Unknown",
                Department = course.Department ?? "Unknown",
                Campus = m.SelectedCampus ?? "Unknown",
                Year = m.SelectedYear ?? "Unknown"
            });

            await _context.SaveChangesAsync();
        }

        private async Task AddEnrollment_SingleStudent_MultipleCourses(int studentId, int courseId, CourseEnrollmentViewModel m)
        {
            var student = await _context.Students.FindAsync(studentId);
            var course = await _context.Courses.FindAsync(courseId);

            if (student == null || course == null)
                return;

            _context.Enrollment_SingleStudent_MultipleCourses.Add(new Enrollment_SingleStudent_MultipleCourses
            {
                StudentID = studentId,
                StudentName = student.FullName ?? "Unknown",
                CourseID = courseId,
                CourseName = course.CourseName ?? "Unknown",
                Department = course.Department ?? "Unknown",
                Campus = m.SelectedCampus ?? "Unknown",
                Year = m.SelectedYear ?? "Unknown"
            });

            await _context.SaveChangesAsync();
        }

        private async Task AddEnrollment_MultipleStudents_MultipleCourses(int studentId, int courseId, CourseEnrollmentViewModel m)
        {
            var student = await _context.Students.FindAsync(studentId);
            var course = await _context.Courses.FindAsync(courseId);

            if (student == null || course == null)
                return;

            _context.Enrollment_MultipleStudents_MultipleCourses.Add(new Enrollment_MultipleStudents_MultipleCourses
            {
                StudentID = studentId,
                StudentName = student.FullName ?? "Unknown",
                CourseID = courseId,
                CourseName = course.CourseName ?? "Unknown",
                Department = course.Department ?? "Unknown",
                Campus = m.SelectedCampus ?? "Unknown",
                Year = m.SelectedYear ?? "Unknown"
            });

            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsAlreadyEnrolled(int studentId, int courseId)
        {
            return await _context.Enrollment_SingleStudent_SingleCourse
                       .AnyAsync(e => e.StudentID == studentId && e.CourseID == courseId)
                   || await _context.Enrollment_SingleStudent_MultipleCourses
                       .AnyAsync(e => e.StudentID == studentId && e.CourseID == courseId)
                   || await _context.Enrollment_MultipleStudents_SingleCourse
                       .AnyAsync(e => e.StudentID == studentId && e.CourseID == courseId)
                   || await _context.Enrollment_MultipleStudents_MultipleCourses
                       .AnyAsync(e => e.StudentID == studentId && e.CourseID == courseId);
        }

        private IActionResult Success(string msg)
        {
            TempData["Success"] = msg;
            return RedirectToAction(nameof(Index));
        }

        private IActionResult Error(string msg)
        {
            TempData["Error"] = msg;
            return RedirectToAction(nameof(Index));
        }
    }
}
