using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        public int YearPublished { get; set; }

        public string Genre { get; set; } = string.Empty;
    }
}
