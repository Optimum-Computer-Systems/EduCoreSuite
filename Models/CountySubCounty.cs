using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    [Table("Counties")]
    public class County
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Navigation property to SubCounties
        public ICollection<SubCounty> SubCounties { get; set; } = new List<SubCounty>();
    }

    [Table("SubCounties")]
    public class SubCounty
    {
        [Key]
        public int SubCountyID { get; set; }

        [Required]
        public string SubCountyName { get; set; } = string.Empty;

        // Foreign Key
        public int CountyId { get; set; }
        public object CountyID { get; internal set; }

        // Navigation property
        [ForeignKey("CountyId")]
        public County County { get; set; } = null!;
    }
}
