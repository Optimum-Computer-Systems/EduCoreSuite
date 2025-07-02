using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using EduCoreSuite.Models;

namespace EduCoreSuite.Models.ViewModels
{
    public class CourseEnrollmentViewModel
    {
      
        public Course Course { get; set; } = new Course();


        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Programmes { get; set; } = new();
        public List<SelectListItem> StudyLevels { get; set; } = new();
        public List<SelectListItem> Campuses { get; set; } = new();
        public List<SelectListItem> ExamBodies { get; set; } = new();
        public List<SelectListItem> StudyStatuses { get; set; } = new();

        // Additional enrollment-specific fields
        public int SelectedStudentId { get; set; }
        public List<int> SelectedStudentIds { get; set; } = new();
        public List<SelectListItem> Students { get; set; } = new();
    }
}
