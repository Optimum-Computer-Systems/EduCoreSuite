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
                //TotalStudents = await _context.Students.CountAsync(),
                //TotalCourses = await _context.Courses.CountAsync(),
                //TotalEnrollments =
                //    await _context.Enrollment_SingleStudent_SingleCourse.CountAsync()
                //  + await _context.Enrollment_MultipleStudents_SingleCourse.CountAsync()
                //  + await _context.Enrollment_SingleStudent_MultipleCourses.CountAsync()
                //  + await _context.Enrollment_MultipleStudents_MultipleCourses.CountAsync(),
                //TotalSessions = 25, // You can replace this with dynamic logic if available

                RecentActivities = new List<ActivityItem>
                {
                    new ActivityItem
                    {
                        Title = "New Enrollment",
                        TimeAgo = "2 hours ago",
                        Description = "New Student, Ethan Carter, Enrolled in Introduction To Programming."
                    },
                    new ActivityItem
                    {
                        Title = "Course Completion Report",
                        TimeAgo = "4 hours ago",
                        Description = "Completion Rate For Advanced Mathematics Increased By 5%"
                    },
                    new ActivityItem
                    {
                        Title = "New Course Added",
                        TimeAgo = "6 hours ago",
                        Description = "Digital Marketing Fundamentals Added To Catalog"
                    },
                    new ActivityItem
                    {
                        Title = "Exam Completion",
                        TimeAgo = "8 hours ago",
                        Description = "Noah Walker Completed Final Exam For Operating Systems"
                    },
                    new ActivityItem
                    {
                        Title = "System Maintenance",
                        TimeAgo = "10 hours ago",
                        Description = "Maintenance Scheduled Tomorrow, 10pm to 12am"
                    }
                }
            };

            return View(vm);
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
