using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using EduCoreSuite.Models;

namespace EduCoreSuite.Models.ViewModels
{
    public class CourseFormViewModel
    {
        public Course Course { get; set; } = new();

        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Programmes { get; set; } = new();
        public List<SelectListItem> Campuses { get; set; } = new();
        public List<SelectListItem> ExamBodies { get; set; } = new();
        public List<SelectListItem> StudyStatuses { get; set; } = new();
        public List<SelectListItem> StudyModes { get; set; } = new();
    }
}
