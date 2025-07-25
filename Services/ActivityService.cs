using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Services
{
    public class ActivityService
    {
        private readonly ApplicationDbContext _context;

        public ActivityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogStudentActivity(string title, string description, int studentId, string? userId = null, string? userName = null)
        {
            var activity = new SystemActivity
            {
                Title = title,
                Description = description,
                ActivityType = "Student",
                StudentID = studentId,
                UserId = userId,
                UserName = userName,
                Icon = "fas fa-user-graduate",
                IconColor = "text-primary"
            };

            await LogActivity(activity);
        }

        public async Task LogCourseActivity(string title, string description, int courseId, string? userId = null, string? userName = null)
        {
            var activity = new SystemActivity
            {
                Title = title,
                Description = description,
                ActivityType = "Course",
                CourseID = courseId,
                UserId = userId,
                UserName = userName,
                Icon = "fas fa-book",
                IconColor = "text-success"
            };

            await LogActivity(activity);
        }

        public async Task LogCampusActivity(string title, string description, int campusId, string? userId = null, string? userName = null)
        {
            var activity = new SystemActivity
            {
                Title = title,
                Description = description,
                ActivityType = "Campus",
                CampusID = campusId,
                UserId = userId,
                UserName = userName,
                Icon = "fas fa-university",
                IconColor = "text-info"
            };

            await LogActivity(activity);
        }

        public async Task LogStaffActivity(string title, string description, int staffId, string? userId = null, string? userName = null)
        {
            var activity = new SystemActivity
            {
                Title = title,
                Description = description,
                ActivityType = "Staff",
                StaffID = staffId,
                UserId = userId,
                UserName = userName,
                Icon = "fas fa-user-tie",
                IconColor = "text-warning"
            };

            await LogActivity(activity);
        }

        public async Task LogSystemActivity(string title, string description, string? userId = null, string? userName = null)
        {
            var activity = new SystemActivity
            {
                Title = title,
                Description = description,
                ActivityType = "System",
                UserId = userId,
                UserName = userName,
                Icon = "fas fa-cog",
                IconColor = "text-secondary"
            };

            await LogActivity(activity);
        }

        private async Task LogActivity(SystemActivity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SystemActivity>> GetRecentActivities(int count = 10)
        {
            return await _context.Activities
                .OrderByDescending(a => a.Timestamp)
                .Take(count)
                .ToListAsync();
        }
    }
}