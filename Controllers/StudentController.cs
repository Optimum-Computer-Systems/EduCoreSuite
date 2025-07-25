using EduCoreSuite.Data;
using EduCoreSuite.Models;
using EduCoreSuite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EduCoreSuite.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ActivityService _activityService;

        public StudentController(ApplicationDbContext context, ActivityService activityService)
        {
            _context = context;
            _activityService = activityService;
        }

        // GET: Student
        public async Task<IActionResult> Index(string searchTerm, string departmentFilter, string courseFilter, string programmeFilter, string examBodyFilter, string yearFilter, int page = 1, int pageSize = 20)
        {
            // Debug information
            var debugInfo = $"Search: {searchTerm ?? "null"}, Dept: {departmentFilter ?? "null"}, Course: {courseFilter ?? "null"}, Programme: {programmeFilter ?? "null"}, ExamBody: {examBodyFilter ?? "null"}, Year: {yearFilter ?? "null"}";
            TempData["DebugInfo"] = debugInfo;
            
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

        // Other actions remain the same...
    }
}