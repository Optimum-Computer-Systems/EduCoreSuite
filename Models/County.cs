using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class County
    {
        public int CountyID { get; set; }

        [Required]
        [StringLength(100)]
        public string CountyName { get; set; }

        public List<SubCounty> SubCounties { get; set; }
    }
}
