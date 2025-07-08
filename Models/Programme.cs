using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Programme
    {
     
        public int ProgrammeID { get; set; }

        [Required(ErrorMessage = "Programme name is required")]
        [StringLength(100, ErrorMessage = "Programme name can't exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentID { get; set; }

        // Navigation: Each Programme belongs to one Department
        public Department Department { get; set; }
    }
}
