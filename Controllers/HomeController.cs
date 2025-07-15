using EduCoreSuite.Data;
using EduCoreSuite.Models;
using EduCoreSuite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

            //  Current Totals
            int studentCount = await _context.Students.CountAsync();
            int courseCount = await _context.Courses.CountAsync();
            int enrollmentCount = await _context.Enrollments.CountAsync();
            int sessionCount = await _context.Sessions.CountAsync();

            //  Weekly Counts (for growth calculation)
            int prevStudents = await _context.Students
                .CountAsync(s => s.EnrollmentDate >= sevenDaysAgo);
            int prevCourses = await _context.Courses
                .CountAsync(c => c.CreatedAt >= sevenDaysAgo);
            int prevEnrollments = await _context.Enrollments
                .CountAsync(e => e.EnrolledAt >= sevenDaysAgo);
            int prevSessions = await _context.Sessions
                .CountAsync(s => s.ScheduledDate >= sevenDaysAgo);

            //  Load Most Enrolled Courses — null-safe query
            var topCourses = await _context.Enrollments
                .GroupBy(e => e.CourseID)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new CourseStat
                {
                    CourseName = _context.Courses
                        .Where(c => c.CourseID == g.Key)
                        .Select(c => c.CourseName)
                        .FirstOrDefault() ?? "Unknown",
                    StudentCount = g.Count()
                }).ToListAsync();

            //  Load Recent Activity Logs
            var activities = await _context.ActivityLogs
                .OrderByDescending(a => a.Timestamp)
                .Take(5)
                .Select(a => new ActivityItem
                {
                    Title = a.Category,
                    Description = a.Description,
                    TimeAgo = $"{(DateTime.UtcNow - a.Timestamp).Hours} hours ago"
                }).ToListAsync();

            var vm = new DashboardViewModel
            {
                TotalStudents = studentCount,
                TotalCourses = courseCount,
                TotalEnrollments = enrollmentCount,
                TotalSessions = sessionCount,

                StudentGrowthPercent = CalculateGrowth(studentCount, prevStudents),
                CourseGrowthPercent = CalculateGrowth(courseCount, prevCourses),
                EnrollmentGrowthPercent = CalculateGrowth(enrollmentCount, prevEnrollments),
                SessionGrowthPercent = CalculateGrowth(sessionCount, prevSessions),

                TopCourses = topCourses,
                RecentActivities = activities
            };

            return View(vm);
        }

        private double CalculateGrowth(int current, int previous)
        {
            if (previous == 0) return current == 0 ? 0 : 100;
            return Math.Round(((double)(current - previous) / previous) * 100, 2);
        }

        public IActionResult Privacy() => View();

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
