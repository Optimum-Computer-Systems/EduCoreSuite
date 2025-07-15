using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace EduCoreSuite.Models.ViewModels
{
    public class CourseEnrollmentViewModel
    {
        public int SelectedStudentId { get; set; }
        public List<int> SelectedStudentIds { get; set; } = new();
        public int SelectedCourseId { get; set; }
        public List<int> SelectedCourseIds { get; set; } = new();
        public string SelectedDepartment { get; set; } = "";
        public string SelectedCampus { get; set; } = "";
        public string SelectedYear { get; set; } = "";

        public List<SelectListItem> Students { get; set; } = new();
        public List<SelectListItem> Courses { get; set; } = new();
        public List<SelectListItem> Campuses { get; set; } = new();
        public List<SelectListItem> Years { get; set; } = new();
        public required DbSet<CountySubCounty> Counties { get; set; }

    }
}
