using System.ComponentModel.DataAnnotations;
namespace EduCoreSuite.Models
{
    public class StudyMode
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = default!;
        [MaxLength(250)]
        public string? Description { get; set; }

    }
}