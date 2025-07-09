using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        
        public string DepartmentName { get; set; } = string.Empty;

    }
}
