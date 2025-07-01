using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Optimum_University.Models;

namespace Optimum_University.Models.ViewModels
{
    public class CourseFormViewModel
    {
        public Course Course { get; set; } = new();

        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Programmes { get; set; } = new();
        public List<SelectListItem> StudyLevels { get; set; } = new();
        public List<SelectListItem> Campuses { get; set; } = new();
        public List<SelectListItem> ExamBodies { get; set; } = new();
        public List<SelectListItem> StudyStatuses { get; set; } = new();
    }
}
