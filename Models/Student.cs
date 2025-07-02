using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression(@"^([A-Za-z]+\s[A-Za-z]+.*)$", ErrorMessage = "Please enter at least two names")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters")]
        public string FullName { get; set; }

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(07\d{8}|\+2547\d{8})$", ErrorMessage = "Phone number must start with 07 or +2547 and be 10 or 13 digits long")]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; } = string.Empty;
        [Required(ErrorMessage = "Admission Number is required")]

        public string AdmissionNumber { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required")]
        [RegularExpression(@"^(1st Year|2nd Year|3rd Year|4th Year)$", ErrorMessage = "Please select a valid year")]
        public string Year { get; set; } = string.Empty;
        public Student() { }

    }
}
