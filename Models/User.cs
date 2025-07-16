using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace EduCoreSuite.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]//This belongs to the username 
        public String Username { get; set; }

        [Required]

        public String FirstName { get; set; }
        public String LastName { get; set; }

        [Required, EmailAddress]//Email must be required and valid
        public String Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,32}$",
            ErrorMessage = "Password must be between 8 and 32 characters long, contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public String Password { get; set; }

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        [JsonIgnore]
        public Role? Role { get; set; }
    }
}
