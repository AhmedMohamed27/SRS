using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblCourse")]
    public class AspCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key , Column(Order = 1)]
        public int CourseId { get; set; }

        [ForeignKey("AspDepartmentId")]
        public AspDepartment AspDepartment { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int AspDepartmentId { get; set; }

        [Required]
        [MaxLength(150)]
        public string CourseName { get; set; }

        [Required]
        [MaxLength(20)]
        public string CourseTitle { get; set; }


        private int ch = 3;  // Backing store
        [Required]
        public int CreadedHours {
            get
            {
                return ch;
            }
            set
            {
                ch = value;
            }
        }

        [ForeignKey("CourseTimeId")]
        public AspCourseTime AspCourseTime { get; set; }
        [Required]
        public int CourseTimeId { get; set; }

         
    }
}