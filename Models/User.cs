using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCoreSuite.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public long RoleId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [NotMapped]
        public string CustomUserId => Id.ToString("D3");
    }

    public class Role
    {
        public long Id { get; set; }

        [Required, StringLength(50)]
        public string NameOfRole { get; set; } = string.Empty;
    }

    public class Permission
    {
        public long Id { get; set; }

        [Required, StringLength(100)]
        public string NameOfPermission { get; set; } = string.Empty;
    }

    public class RolePermission
    {
        public long Id { get; set; }

        [Required]
        public long RoleID { get; set; }

        [Required]
        public long PermissionID { get; set; }
    }

    public class Course
    {
        public long Id { get; set; }

        [StringLength(100)]
        public string? CourseName { get; set; }

        [StringLength(20)]
        public string? CourseCode { get; set; }

        public long? InstructorID { get; set; }
    }

    public class Registration
    {
        public long Id { get; set; }

        public long StudentID { get; set; }
        public long CourseID { get; set; }
    }
}