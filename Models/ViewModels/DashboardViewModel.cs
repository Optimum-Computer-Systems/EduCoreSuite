namespace EduCoreSuite.Models.ViewModels
{
    public class DashboardViewModel
    {
        //  Dashboard Metrics
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalEnrollments { get; set; }
        public int TotalSessions { get; set; }

        //  Growth Percentages
        public double StudentGrowthPercent { get; set; }
        public double CourseGrowthPercent { get; set; }
        public double EnrollmentGrowthPercent { get; set; }
        public double SessionGrowthPercent { get; set; }

        //  Recent Activities
        public List<ActivityItem> RecentActivities { get; set; } = new();

        //  Most Enrolled Courses
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
