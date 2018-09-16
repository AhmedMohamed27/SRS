using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblDoctorCourses")]
    public class AspDoctorCourses
    {
        public int Id { get; set; }

        [ForeignKey("UserId,RoleId")]
        public AspUser User { get; set; }
        [Required]
        public int UserId { get; set; }

        [NotMapped]
        public string FirstName { get; set; }

        [NotMapped]
        public string LastName { get; set; }

        [NotMapped]
        public string Email { get; set; }

        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        public string AuthKey { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("CourseId,DepId")]
        public AspCourse Course { get; set; }
        [Required]
        [NotMapped]
        public string CourseName { get; set; }
        [NotMapped]
        public string DeptName { get; set; }
        [NotMapped]
        public string CourseTit { get; set; }
        public int CourseId { get; set; }
        [Required]
        public int DepId { get; set; }
    }
}