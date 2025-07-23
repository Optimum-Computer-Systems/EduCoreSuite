using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Institution
    {
        [Key]
        public int InstitutionID { get; set; }

        [Required]
        [StringLength(200)]
        public string InstitutionName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string ContactNumber { get; set; }

        public int? CountyID { get; set; }
        public County County { get; set; }

        public int? SubCountyID { get; set; }
        public SubCounty SubCounty { get; set; }
    }
}
