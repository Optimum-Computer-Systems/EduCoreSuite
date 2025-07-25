using EduCoreSuite.Data;
using EduCoreSuite.Models;
using EduCoreSuite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EduCoreSuite.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExportService _exportService;

        public ReportsController(ApplicationDbContext context, ExportService exportService)
        {
            _context = context;
            _exportService = exportService;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var reportData = new
            {
                TotalStudents = await _context.Students.CountAsync(),
                TotalStaff = await _context.Staff.CountAsync(),
                TotalCourses = await _context.Courses.CountAsync(),
                TotalCampuses = await _context.Campuses.CountAsync(),
                TotalFaculties = await _context.Faculties.CountAsync(),
                TotalDepartments = await _context.Departments.CountAsync(),
                
                // Student Statistics by Department
                StudentsByDepartment = await _context.Students
                    .GroupBy(s => s.Department)
                    .Select(g => new { Department = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToListAsync(),
                
                // Student Statistics by Programme
                StudentsByProgramme = await _context.Students
                    .GroupBy(s => s.Program)
                    .Select(g => new { Programme = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToListAsync(),
                
                // Student Statistics by Year
                StudentsByYear = await _context.Students
                    .GroupBy(s => s.Year)
                    .Select(g => new { Year = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Year)
                    .ToListAsync(),
                
                // Recent Activities
                RecentActivities = await _context.Activities
                    .OrderByDescending(a => a.Timestamp)
                    .Take(10)
                    .ToListAsync()
            };

            return View(reportData);
        }

        // Export Students to Excel
        public async Task<IActionResult> ExportStudentsToExcel(string? searchTerm = null, string? departmentFilter = null, string? programmeFilter = null)
        {
            var excelData = await _exportService.ExportStudentsToExcel(searchTerm, departmentFilter, programmeFilter);
            var fileName = $"Students_Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        // Export Students to CSV
        public async Task<IActionResult> ExportStudentsToCsv(string? searchTerm = null, string? departmentFilter = null, string? programmeFilter = null)
        {
            var csvData = await _exportService.ExportStudentsToCsv(searchTerm, departmentFilter, programmeFilter);
            var fileName = $"Students_Report_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            return File(Encoding.UTF8.GetBytes(csvData), "text/csv", fileName);
        }


    }
}