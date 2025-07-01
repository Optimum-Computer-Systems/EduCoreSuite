namespace EduCoreSuite.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Programme { get; set; } = string.Empty;
        public string StudyLevel { get; set; } = string.Empty;
        public string ExamBody { get; set; } = string.Empty;
        public string StudyStatus { get; set; } = string.Empty;
        public string Campus { get; set; } = string.Empty;

        public Course() { }
    }
}
