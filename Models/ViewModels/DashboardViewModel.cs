namespace EduCoreSuite.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalEnrollments { get; set; }
        public int TotalSessions { get; set; }

        public List<ActivityItem> RecentActivities { get; set; } = new();
        public List<CourseStat> TopCourses { get; set; } = new();
    }

    public class ActivityItem
    {
        public string Title { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CourseStat
    {
        public string CourseName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
    }
}