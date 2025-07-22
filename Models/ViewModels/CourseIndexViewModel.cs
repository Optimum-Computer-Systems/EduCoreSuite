using System.Collections.Generic;

namespace EduCoreSuite.Models.ViewModels
{
    public class CourseIndexViewModel
    {
        public Dictionary<Department, List<Course>> CoursesByDepartment { get; set; } = new Dictionary<Department, List<Course>>();
        public string DepartmentSearch { get; set; } = string.Empty;
        public string CourseSearch { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int CoursesPerDepartment { get; set; } = 10;
    }
}