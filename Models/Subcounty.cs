using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Subcounty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCountyID { get; set; }

        [Required]
        [StringLength(100)]
        public string SubCountyName { get; set; }

        [Required]
        // Foreign Key property linking to the County table
        public int CountyID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property for the parent County (Many-to-One relationship)
        // The [ForeignKey("CountyID")] attribute explicitly defines the foreign key column.
        [ForeignKey("CountyID")]
        public County County { get; set; }

        // Navigation property for related Institutions (One-to-Many relationship)
        public ICollection<Institution> Institutions { get; set; }
    }
}
    

