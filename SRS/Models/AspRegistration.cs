using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblRegis")]
    public class AspRegistration
    {
        [Key,Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisID { get; set; }



        [ForeignKey("UserId,AspRoleId")]
        public AspUser AspUser { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int UserId { get; set; }
        [Required]
        [Key, Column(Order = 3)]
        public int AspRoleId { get; set; }



        [ForeignKey("CourseId,AspDepartmentId")]
        public AspCourse AspCourse { get; set; }
        [Required]
        [Key, Column(Order = 4)]
        public int CourseId { get; set; }
        [Required]
        [Key, Column(Order = 5)]
        public int AspDepartmentId { get; set; }

        [Required]
        [Key, Column(Order = 6)]
        
        public string Year { get; set; }


        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegisterCreated{ get; set; }
        
        [Required]
        public string Semester { get; set; }
    }
}