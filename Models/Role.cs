using System.Text.Json.Serialization;

namespace EduCoreSuite.Models
{
    public class Role
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
