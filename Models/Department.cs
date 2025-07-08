using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Department
    {
       
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name can't exceed 100 characters")]
        public string Name { get; set; }

        // Navigation: One Department has many Programmes
        public ICollection<Programme> Programmes { get; set; }
    }
}
