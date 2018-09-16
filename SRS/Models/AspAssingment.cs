using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblAssingment")]
    public class AspAssingment
    {
        [Key,Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssinId { get; set; }

        [ForeignKey("AspUserId,AspRoleId")]
        public AspUser AspUser { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int AspUserId { get; set; }
        [Required]
        [Key, Column(Order = 3)]
        public int AspRoleId { get; set; }



        
        public AspCourse AspCourse { get; set; }
        [Required]
        public int AspCourseId { get; set; }
        [Required]
        public int AspDepartmentId { get; set; }
        [Required]
        public int AspCoursesId { get; set; }



        [Required]
        [Key, Column(Order = 4)]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime Deadline { get; set; }
    }
}