using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Institution
    {

        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstitutionID { get; set; }

        [Required]
        [StringLength(255)] 
        public string InstitutionName { get; set; }

        [StringLength(255)]
        [EmailAddress] 
        public string Email { get; set; }

        [StringLength(50)]
        public string ContactNumber { get; set; }

        [Required]
        // Foreign Key property for SubCounty
        public int SubCountyID { get; set; }

        [Required]
        public bool Accredited { get; set; } = false; 

        [Required]
        [DataType(DataType.DateTime)] 
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        [ForeignKey("SubCountyID")] 
        public  int Id { get; set; }
        public object Subcounty { get; internal set; }
    }
}
