using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        // ----------- Personal Details -----------
        [Required, RegularExpression(@"^([A-Za-z]+\s[A-Za-z]+.*)$", ErrorMessage = "Please enter at least two names")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public string DOB { get; set; } = string.Empty;

        [Required] public string Religion { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string Gender { get; set; } = string.Empty;
        [Required] public string Disability { get; set; } = string.Empty;
        [Required] public string AdmissionNumber { get; set; } = string.Empty;
        [Required] public string Nationality { get; set; } = string.Empty;
        [Required] public string Medical { get; set; } = string.Empty;
        [Required, RegularExpression(@"^\d{8,}$")] public string IDNumber { get; set; } = string.Empty;
        [Required] public string MaritalStatus { get; set; } = string.Empty;
        [Required] public string CoCurricular { get; set; } = string.Empty;

        // ----------- Contact Info -----------
        [Required, RegularExpression(@"^(\+?\d{10,13})$")] public string PrimaryPhone { get; set; } = string.Empty;
        [Required, RegularExpression(@"^(\+?\d{10,13})$")] public string AltPhone { get; set; } = string.Empty;
        [Required, RegularExpression(@"^\d{5}$")] public string PostalCode { get; set; } = string.Empty;

        // 👉 NEW: foreign keys instead of raw strings
        [Required] public int CountyID { get; set; }
        [Required] public int SubCountyID { get; set; }

        // navigation props (optional when posting, useful when reading)
        public CountySubCounty? County { get; set; }
        public SubCounty? SubCounty { get; set; }

        [Required, RegularExpression(@"^[A-Za-z\s\-]+$")] public string Town { get; set; } = string.Empty;
        [Required, RegularExpression(@"^[A-Za-z\s\-]+$")] public string Ward { get; set; } = string.Empty;

        // ----------- Emergency Contact -----------
        [Required, RegularExpression(@"^([A-Za-z]+\s[A-Za-z]+.*)$")] public string EmergencyName { get; set; } = string.Empty;
        [Required] public string Relationship { get; set; } = string.Empty;
        [Required, RegularExpression(@"^(\+?\d{10,13})$")] public string EmergencyPhone { get; set; } = string.Empty;
        [Required, EmailAddress] public string EmergencyEmail { get; set; } = string.Empty;
        [Required] public string EmergencyAddress { get; set; } = string.Empty;

        // ----------- Academic Details -----------
        [Required] public string Course { get; set; } = string.Empty;
        [Required] public string Department { get; set; } = string.Empty;
        [Required] public string Faculty { get; set; } = string.Empty;
        [Required, RegularExpression(@"^(Certificate|Diploma|Degree|Masters)$")]
        public string Program { get; set; } = string.Empty;
        [Required] public string ExamBody { get; set; } = string.Empty;
        [Required, RegularExpression(@"^(1st Year|2nd Year|3rd Year|4th Year)$")]
        public string Year { get; set; } = string.Empty;
    }
}
