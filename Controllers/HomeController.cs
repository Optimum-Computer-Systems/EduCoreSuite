using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;
using EduCoreSuite.Models.ViewModels;

namespace EduCoreSuite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForgeDBContext _context;

        public HomeController(ILogger<HomeController> logger, ForgeDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                // Key Metrics
                TotalStudents = await _context.Students.CountAsync(),
                TotalCourses = await _context.Courses.CountAsync(),
                TotalCampuses = await _context.Campuses.CountAsync(c => c.IsActive),
                TotalStaff = await _context.Staff.CountAsync(),

                // Enrollment by Academic Year
                EnrollmentByAcademicYear = await GetEnrollmentByAcademicYear(),

                // Students by Department
                StudentsByDepartment = await GetStudentsByDepartment(),

                // Enrollment by Program Type
                EnrollmentByProgramType = await GetEnrollmentByProgramType(),

                // Recent Activities - now using real-time data
                RecentActivities = await GetRecentActivities()
            };

            return View(vm);
        }

        // Removed GetEnrollmentTrends and related methods as they're no longer needed

        private async Task<List<AcademicYearData>> GetEnrollmentByAcademicYear()
        {
            // Kenyan academic years: 1st Year, 2nd Year, 3rd Year, 4th Year
            var colors = new[] { "#4e73df", "#1cc88a", "#36b9cc", "#f6c23e", "#e74a3b" };
            var yearLevels = new[] { "1st Year", "2nd Year", "3rd Year", "4th Year", "Other" };
            
            var totalStudents = await _context.Students.CountAsync();
            
            // Group students by academic year
            var yearStats = await _context.Students
                .GroupBy(s => s.Year)
                .Select(g => new 
                {
                    YearLevel = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
                
            var result = new List<AcademicYearData>();
            
            // Process each standard year level
            foreach (var year in yearLevels)
            {
                var yearStat = yearStats.FirstOrDefault(y => y.YearLevel == year);
                var count = yearStat?.Count ?? 0;
                
                result.Add(new AcademicYearData
                {
                    YearLevel = year,
                    StudentCount = count,
                    Color = colors[Array.IndexOf(yearLevels, year)],
                    Percentage = totalStudents > 0 ? Math.Round((double)count / totalStudents * 100, 1) : 0
                });
            }
            
            // Add any non-standard years to "Other" category
            var otherYears = yearStats.Where(y => !yearLevels.Contains(y.YearLevel)).ToList();
            if (otherYears.Any())
            {
                var otherCount = otherYears.Sum(y => y.Count);
                var otherEntry = result.FirstOrDefault(r => r.YearLevel == "Other");
                if (otherEntry != null)
                {
                    otherEntry.StudentCount += otherCount;
                    otherEntry.Percentage = totalStudents > 0 ? Math.Round((double)otherEntry.StudentCount / totalStudents * 100, 1) : 0;
                }
            }
            
            return result.OrderBy(y => y.YearLevel).ToList();
        }

        private async Task<List<DepartmentStudentData>> GetStudentsByDepartment()
        {
            var colors = new[] { "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF", "#FF9F40" };
            
            var departmentStats = await _context.Students
                .GroupBy(s => s.Department)
                .Select(g => new DepartmentStudentData
                {
                    DepartmentName = g.Key,
                    StudentCount = g.Count()
                })
                .OrderByDescending(d => d.StudentCount)
                .Take(8) // Show top 8 departments
                .ToListAsync();

            // Assign colors
            for (int i = 0; i < departmentStats.Count; i++)
            {
                departmentStats[i].Color = colors[i % colors.Length];
            }

            return departmentStats;
        }
        
        private async Task<List<ProgramTypeData>> GetEnrollmentByProgramType()
        {
            // Program types in Kenyan education system
            var programTypes = new[] { "Certificate", "Diploma", "Degree", "Masters" };
            var colors = new[] { "#1cc88a", "#4e73df", "#f6c23e", "#e74a3b" };
            
            var totalStudents = await _context.Students.CountAsync();
            
            // Group students by program type
            var programStats = await _context.Students
                .GroupBy(s => s.Program)
                .Select(g => new 
                {
                    ProgramType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
                
            var result = new List<ProgramTypeData>();
            
            // Process each program type
            foreach (var program in programTypes)
            {
                var programStat = programStats.FirstOrDefault(p => p.ProgramType == program);
                var count = programStat?.Count ?? 0;
                
                result.Add(new ProgramTypeData
                {
                    ProgramType = program,
                    StudentCount = count,
                    Color = colors[Array.IndexOf(programTypes, program)],
                    Percentage = totalStudents > 0 ? Math.Round((double)count / totalStudents * 100, 1) : 0
                });
            }
            
            return result;
        }

        // Campus distribution data is now handled differently in the view

        private async Task<List<ActivityItem>> GetRecentActivities()
        {
            // Get activities from the database using ActivityService
            var activityService = new Services.ActivityService(_context);
            var activities = await activityService.GetRecentActivities(10);
            
            
            
            // Convert to ActivityItem view model
            return activities.Select(a => new ActivityItem
            {
                Title = a.Title,
                Description = a.Description,
                TimeAgo = GetTimeAgo(a.Timestamp),
                Icon = a.Icon,
                IconColor = a.IconColor
            }).ToList();
        }
        
        private string GetTimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;
            
            if (timeSpan.TotalMinutes < 1)
                return "just now";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minute{(timeSpan.TotalMinutes == 1 ? "" : "s")} ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hour{(timeSpan.TotalHours == 1 ? "" : "s")} ago";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} day{(timeSpan.TotalDays == 1 ? "" : "s")} ago";
            if (timeSpan.TotalDays < 30)
                return $"{(int)(timeSpan.TotalDays / 7)} week{((int)(timeSpan.TotalDays / 7) == 1 ? "" : "s")} ago";
            
            return dateTime.ToString("MMM dd, yyyy");
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
