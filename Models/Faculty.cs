using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Faculty
    {
        public int Id { get; set; }

        [Required]
        public string FacultyName { get; set; } = string.Empty;

    }
}
