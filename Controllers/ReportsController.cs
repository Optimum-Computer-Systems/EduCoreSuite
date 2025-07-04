using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models.ViewModels;
using EduCoreSuite.Models.EnrollmentModes;
using System.Text;

namespace EduCoreSuite.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ForgeDBContext _context;

        public ReportsController(ForgeDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? department, int? studentId, int? courseId, int page = 1)
        {
            Console.WriteLine($"Received filters - Department: '{department}', StudentID: {studentId}, CourseID: {courseId}");

            const int PageSize = 10;

            var combinedQuery = _context.Enrollment_SingleStudent_SingleCourse.Select(e => new { e.StudentID, e.StudentName, e.CourseID, e.CourseName, e.Department, e.Campus, e.Year, e.EnrollmentDate })
                .Concat(_context.Enrollment_SingleStudent_MultipleCourses.Select(e => new { e.StudentID, e.StudentName, e.CourseID, e.CourseName, e.Department, e.Campus, e.Year, e.EnrollmentDate }))
                .Concat(_context.Enrollment_MultipleStudents_SingleCourse.Select(e => new { e.StudentID, e.StudentName, e.CourseID, e.CourseName, e.Department, e.Campus, e.Year, e.EnrollmentDate }))
                .Concat(_context.Enrollment_MultipleStudents_MultipleCourses.Select(e => new { e.StudentID, e.StudentName, e.CourseID, e.CourseName, e.Department, e.Campus, e.Year, e.EnrollmentDate }));

            if (!string.IsNullOrEmpty(department))
                combinedQuery = combinedQuery.Where(e => e.Department.ToLower() == department.ToLower());

            if (studentId.HasValue && studentId > 0)
                combinedQuery = combinedQuery.Where(e => e.StudentID == studentId);

            if (courseId.HasValue && courseId > 0)
                combinedQuery = combinedQuery.Where(e => e.CourseID == courseId);

            var totalCount = await combinedQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            var rawResults = await combinedQuery
                .OrderBy(e => e.StudentName)
                .ThenBy(e => e.CourseName)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var pagedResults = rawResults.Select(e => new EnrollmentReportRow
            {
                StudentId = e.StudentID.ToString(),
                StudentName = e.StudentName ?? "N/A",
                CourseName = e.CourseName ?? "N/A",
                DepartmentName = e.Department ?? "N/A",
                Campus = e.Campus ?? "N/A",
                Year = int.TryParse(e.Year, out var y) ? y : 0,
                Status = "Enrolled"
            }).ToList();

            var departments = await _context.Courses
                .Where(c => c.Department != null)
                .Select(c => c.Department)
                .Distinct()
                .OrderBy(d => d)
                .Select(d => new SelectListItem { Value = d, Text = d })
                .ToListAsync();

            var viewModel = new ReportFilterViewModel
            {
                SelectedDepartment = department,
                SelectedStudentId = studentId,
                SelectedCourseId = courseId,
                Departments = departments,
                Students = await _context.Students
                    .OrderBy(s => s.FullName)
                    .Select(s => new SelectListItem
                    {
                        Value = s.StudentID.ToString(),
                        Text = $"{s.FullName} (ID: {s.StudentID})",
                        Selected = s.StudentID == studentId
                    }).ToListAsync(),
                Courses = await _context.Courses
                    .OrderBy(c => c.CourseName)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseID.ToString(),
                        Text = $"{c.CourseName} (ID: {c.CourseID})",
                        Selected = c.CourseID == courseId
                    }).ToListAsync(),
                ReportResults = pagedResults,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadReport(string? department, int? studentId, int? courseId)
        {
            var normalizedDept = department?.Trim().ToLower();

            var combinedQuery = _context.Enrollment_SingleStudent_SingleCourse.Select(e => new Enrollment_SingleStudent_SingleCourse
            {
                StudentID = e.StudentID,
                StudentName = e.StudentName,
                CourseID = e.CourseID,
                CourseName = e.CourseName,
                Department = e.Department,
                Campus = e.Campus,
                Year = e.Year,
                EnrollmentDate = e.EnrollmentDate
            })
            .Concat(_context.Enrollment_SingleStudent_MultipleCourses.Select(e => new Enrollment_SingleStudent_SingleCourse
            {
                StudentID = e.StudentID,
                StudentName = e.StudentName,
                CourseID = e.CourseID,
                CourseName = e.CourseName,
                Department = e.Department,
                Campus = e.Campus,
                Year = e.Year,
                EnrollmentDate = e.EnrollmentDate
            }))
            .Concat(_context.Enrollment_MultipleStudents_SingleCourse.Select(e => new Enrollment_SingleStudent_SingleCourse
            {
                StudentID = e.StudentID,
                StudentName = e.StudentName,
                CourseID = e.CourseID,
                CourseName = e.CourseName,
                Department = e.Department,
                Campus = e.Campus,
                Year = e.Year,
                EnrollmentDate = e.EnrollmentDate
            }))
            .Concat(_context.Enrollment_MultipleStudents_MultipleCourses.Select(e => new Enrollment_SingleStudent_SingleCourse
            {
                StudentID = e.StudentID,
                StudentName = e.StudentName,
                CourseID = e.CourseID,
                CourseName = e.CourseName,
                Department = e.Department,
                Campus = e.Campus,
                Year = e.Year,
                EnrollmentDate = e.EnrollmentDate
            })).AsQueryable();

            if (!string.IsNullOrEmpty(normalizedDept))
                combinedQuery = combinedQuery.Where(e => e.Department.ToLower().Trim() == normalizedDept);

            if (studentId.HasValue)
                combinedQuery = combinedQuery.Where(e => e.StudentID == studentId.Value);

            if (courseId.HasValue)
                combinedQuery = combinedQuery.Where(e => e.CourseID == courseId.Value);

            var rows = await combinedQuery.OrderBy(e => e.EnrollmentDate).ToListAsync();

            var csvBytes = GenerateCsv(rows);
            return File(csvBytes, "text/csv", "EnrollmentReport.csv");
        }

        private byte[] GenerateCsv(List<Enrollment_SingleStudent_SingleCourse> rows)
        {
            var sb = new StringBuilder();
            sb.AppendLine("StudentID,StudentName,CourseName,Department,Campus,Year,Status");
            foreach (var row in rows)
            {
                sb.AppendLine($"\"{row.StudentID}\",\"{EscapeCsvValue(row.StudentName)}\",\"{EscapeCsvValue(row.CourseName)}\",\"{EscapeCsvValue(row.Department)}\",\"{EscapeCsvValue(row.Campus)}\",\"{row.Year}\",\"Enrolled\"");
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private string EscapeCsvValue(string? value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return value.Replace("\"", "\"\"");
        }
    }
}
