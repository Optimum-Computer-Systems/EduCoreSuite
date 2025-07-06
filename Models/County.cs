using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class County
    {
        [Key]
        [Required]
        public int CountyID { get; set; }
        public string CountyName { get; set; }

        public ICollection<Subcounty> SubCounties { get; set; }


    }
}
