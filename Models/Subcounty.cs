using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class SubCounty
    {
        public int SubCountyID { get; set; }

        [Required]
        [StringLength(100)]
        public string SubCountyName { get; set; }

        public int CountyID { get; set; }
        public County County { get; set; }
    }
}
