using System.ComponentModel.DataAnnotations;

namespace EduCoreSuite.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        // ------------------ Personal Details ------------------
        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression(@"^([A-Za-z]+\s[A-Za-z]+.*)$", ErrorMessage = "Please enter at least two names")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required")]
        public string DOB { get; set; } = string.Empty;

        [Required(ErrorMessage = "Religion is required")]
        public string Religion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Disability status is required")]
        public string Disability { get; set; } = string.Empty;

        [Required(ErrorMessage = "Admission Number is required")]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Admission number can include characters and numbers only")]
        public string AdmissionNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nationality is required")]
        public string Nationality { get; set; } = string.Empty;

        [Required(ErrorMessage = "Medical field is required")]
        public string Medical { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID Number is required")]
        [RegularExpression(@"^\d{8,}$", ErrorMessage = "ID Number must be at least 8 digits and numeric only")]
        public string IDNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Marital Status is required")]
        public string MaritalStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Co-Curricular field is required")]
        public string CoCurricular { get; set; } = string.Empty;

        // ------------------ Contact Info ------------------
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(\+?\d{10,13})$", ErrorMessage = "Phone must be 10–13 digits and can start with +")]
        public string PrimaryPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Alternative phone number is required")]
        [RegularExpression(@"^(\+?\d{10,13})$", ErrorMessage = "Phone must be 10–13 digits and can start with +")]
        public string AltPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal code is required")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Enter a valid 5-digit Kenyan postal code")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "County is required")]
        public string County { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sub-County is required")]
        public string SubCounty { get; set; } = string.Empty;

        [Required(ErrorMessage = "Town/Estate is required")]
        [RegularExpression(@"^[A-Za-z\s\-]+$", ErrorMessage = "Only letters allowed")]
        public string Town { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ward is required")]
        [RegularExpression(@"^[A-Za-z\s\-]+$", ErrorMessage = "Only letters allowed")]
        public string Ward { get; set; } = string.Empty;

        // ------------------ Emergency Contact ------------------
        [Required(ErrorMessage = "Emergency contact name is required")]
        [RegularExpression(@"^([A-Za-z]+\s[A-Za-z]+.*)$", ErrorMessage = "Please enter at least two names")]
        public string EmergencyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Relationship is required")]
        public string Relationship { get; set; } = string.Empty;

        [Required(ErrorMessage = "Emergency contact phone is required")]
        [RegularExpression(@"^(\+?\d{10,13})$", ErrorMessage = "Phone must be 10–13 digits and can start with +")]
        public string EmergencyPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Emergency email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string EmergencyEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Emergency address is required")]
        public string EmergencyAddress { get; set; } = string.Empty;

        // ------------------ Academic Details ------------------
        [Required(ErrorMessage = "Course is required")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Faculty is required")]
        public string Faculty { get; set; } = string.Empty;

        [Required(ErrorMessage = "Program is required")]
        [RegularExpression(@"^(Certificate|Diploma|Degree|Masters)$", ErrorMessage = "Select a valid program")]
        public string Program { get; set; } = string.Empty;

        [Required(ErrorMessage = "Exam body is required")]
        public string ExamBody { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required")]
        [RegularExpression(@"^(1st Year|2nd Year|3rd Year|4th Year)$", ErrorMessage = "Please select a valid year")]
        public string Year { get; set; } = string.Empty;

        // Constructor
        public Student() { }
    }
}
