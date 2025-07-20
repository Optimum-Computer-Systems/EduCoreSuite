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

                // Enrollment Trends (simulated monthly data)
                EnrollmentTrends = GetEnrollmentTrends(),

                // Students by Department
                StudentsByDepartment = await GetStudentsByDepartment(),

                // Campus Distribution
                CampusDistribution = await GetCampusDistribution(),

                // Recent Activities
                RecentActivities = GetRecentActivities()
            };

            return View(vm);
        }

        private List<MonthlyEnrollmentData> GetEnrollmentTrends()
        {
            // Simulated enrollment trends for the last 12 months
            var currentDate = DateTime.Now;
            var trends = new List<MonthlyEnrollmentData>();

            for (int i = 11; i >= 0; i--)
            {
                var date = currentDate.AddMonths(-i);
                var random = new Random(date.Month + date.Year);
                trends.Add(new MonthlyEnrollmentData
                {
                    Month = date.ToString("MMM yyyy"),
                    StudentCount = random.Next(15, 45), // Random enrollment between 15-45
                    Year = date.Year
                });
            }

            return trends;
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
                .Take(6)
                .ToListAsync();

            // Assign colors
            for (int i = 0; i < departmentStats.Count; i++)
            {
                departmentStats[i].Color = colors[i % colors.Length];
            }

            return departmentStats;
        }

        private async Task<List<CampusDistributionData>> GetCampusDistribution()
        {
            var colors = new[] { "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF" };
            
            // Get campus names and simulate student distribution
            var campuses = await _context.Campuses
                .Where(c => c.IsActive)
                .Select(c => c.Name)
                .ToListAsync();

            var campusDistribution = new List<CampusDistributionData>();
            var random = new Random();

            for (int i = 0; i < campuses.Count; i++)
            {
                campusDistribution.Add(new CampusDistributionData
                {
                    CampusName = campuses[i],
                    StudentCount = random.Next(50, 200), // Simulated student count
                    Color = colors[i % colors.Length]
                });
            }

            return campusDistribution.OrderByDescending(c => c.StudentCount).ToList();
        }

        private List<ActivityItem> GetRecentActivities()
        {
            return new List<ActivityItem>
            {
                new ActivityItem
                {
                    Title = "New Student Registration",
                    TimeAgo = "2 minutes ago",
                    Description = "John Doe registered for Computer Science program",
                    Icon = "fas fa-user-plus",
                    IconColor = "text-success"
                },
                new ActivityItem
                {
                    Title = "Course Added",
                    TimeAgo = "1 hour ago",
                    Description = "Advanced Database Systems added to catalog",
                    Icon = "fas fa-book",
                    IconColor = "text-primary"
                },
                new ActivityItem
                {
                    Title = "Campus Updated",
                    TimeAgo = "3 hours ago",
                    Description = "Main Campus information updated",
                    Icon = "fas fa-university",
                    IconColor = "text-info"
                },
                new ActivityItem
                {
                    Title = "Staff Assignment",
                    TimeAgo = "5 hours ago",
                    Description = "Dr. Smith assigned to Engineering Department",
                    Icon = "fas fa-user-tie",
                    IconColor = "text-warning"
                },
                new ActivityItem
                {
                    Title = "System Backup",
                    TimeAgo = "8 hours ago",
                    Description = "Daily system backup completed successfully",
                    Icon = "fas fa-database",
                    IconColor = "text-secondary"
                },
                new ActivityItem
                {
                    Title = "Report Generated",
                    TimeAgo = "12 hours ago",
                    Description = "Monthly enrollment report generated",
                    Icon = "fas fa-chart-bar",
                    IconColor = "text-primary"
                }
            };
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
