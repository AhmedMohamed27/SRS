using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblPreRequis")]
    public class AspPreRequisCourse
    {
        [Key,Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("CourseId,CourseDepId")]
        public AspCourse Course { get; set; }
        // ------------------------------------------------
        [Key,Column(Order = 1)]
        [Required]
        public int CourseId { get; set; }
        // ------------------------------------------------
        [Required]
        [Key, Column(Order = 2)]
        public int CourseDepId { get; set; }
        // ------------------------------------------------
        [NotMapped]
        public string CourseName { get; set; }
        // ------------------------------------------------
        [NotMapped]
        public string CourseTit { get; set; }
        // ------------------------------------------------


        [ForeignKey("PreRquisCourseId,PreRquisCourseDepId")]
        public AspCourse PreRquisCourse { get; set; }
        // ------------------------------------------------
        [Required]
        [Key, Column(Order = 3)]
        public int PreRquisCourseId { get; set; }
        // ------------------------------------------------
        [Required]
        [Key, Column(Order = 4)]
        public int PreRquisCourseDepId { get; set; }
        // ------------------------------------------------
        [NotMapped]
        public string PreCourseName { get; set; }
        // ------------------------------------------------
        [NotMapped]
        public string PreCourseTit { get; set; }
    }
}