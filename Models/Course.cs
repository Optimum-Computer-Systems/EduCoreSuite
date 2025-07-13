using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        [Required, StringLength(200)]
        public string CourseName { get; set; } = string.Empty;

        // Foreign Keys & Navigation Properties
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; } = null!;

        [Display(Name = "Programme")]
        public int ProgrammeID { get; set; }
        public Programme Programme { get; set; } = null!;

        [Display(Name = "Campus")]
        public int CampusID { get; set; }
        public Campus Campus { get; set; } = null!;

        [Display(Name = "Exam Body")]
        public int ExamBodyID { get; set; }
        public ExamBody ExamBody { get; set; } = null!;

        [Display(Name = "Study Status")]
        public int StudyStatusID { get; set; }
        public StudyStatus StudyStatus { get; set; } = null!;

        [Display(Name = "Study Mode")]
        public int StudyModeID { get; set; }
        public StudyMode StudyMode { get; set; } = null!;

        public Course() { }
    }
}
