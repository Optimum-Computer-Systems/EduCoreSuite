using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    [Table("Counties")]
    public class CountySubCounty
    {
        [Key] public int CountyID { get; set; }

        [Required] public string CountyName { get; set; } = string.Empty;

        // Nav‑prop
        public ICollection<SubCounty> SubCounties { get; set; } = new List<SubCounty>();
    }

    [Table("SubCounties")]
    public class SubCounty
    {
        [Key] public int SubCountyID { get; set; }

        [Required] public string SubCountyName { get; set; } = string.Empty;

        // FK + nav‑prop
        public int CountyID { get; set; }
        public CountySubCounty County { get; set; } = null!;
    }
}
