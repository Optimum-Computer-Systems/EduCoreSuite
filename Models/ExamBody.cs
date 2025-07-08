using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class ExamBody
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }
    }
}
