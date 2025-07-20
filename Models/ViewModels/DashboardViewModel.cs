namespace EduCoreSuite.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Key Metrics
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalCampuses { get; set; }
        public int TotalStaff { get; set; }

        // Chart Data
        public List<MonthlyEnrollmentData> EnrollmentTrends { get; set; } = new();
        public List<DepartmentStudentData> StudentsByDepartment { get; set; } = new();
        public List<CampusDistributionData> CampusDistribution { get; set; } = new();
        
        // Activity Feed
        public List<ActivityItem> RecentActivities { get; set; } = new();
        
        // Additional Stats
        public List<CourseStat> TopCourses { get; set; } = new();
    }

    public class MonthlyEnrollmentData
    {
        public string Month { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public int Year { get; set; }
    }

    public class DepartmentStudentData
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public string Color { get; set; } = string.Empty;
    }

    public class CampusDistributionData
    {
        public string CampusName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public string Color { get; set; } = string.Empty;
    }

    public class ActivityItem
    {
        public string Title { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = "fas fa-info-circle";
        public string IconColor { get; set; } = "text-primary";
    }

    public class CourseStat
    {
        public string CourseName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
    }
}