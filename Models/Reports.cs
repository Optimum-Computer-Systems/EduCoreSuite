using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduCoreSuite.Models.ViewModels
{
    public class ReportFilterViewModel
    {
        public string? SelectedDepartment { get; set; }
        public int? SelectedStudentId { get; set; }
        public int? SelectedCourseId { get; set; }
        public int? SelectedCampusId { get; set; }  // ✅ NEW

        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Students { get; set; } = new();
        public List<SelectListItem> Courses { get; set; } = new();
        public List<SelectListItem> Campuses { get; set; } = new();  // ✅ NEW

        public List<EnrollmentReportRow> ReportResults { get; set; } = new();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class EnrollmentReportRow
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string Campus { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
