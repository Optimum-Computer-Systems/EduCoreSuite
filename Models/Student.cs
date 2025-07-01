namespace EduCoreSuite.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string AdmissionNumber { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public Student (){ }

    }
}
