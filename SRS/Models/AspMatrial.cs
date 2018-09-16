using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblMatrial")]
    public class AspMatrial
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public string FilePath { get; set; }

        [ForeignKey("AspCourseId,AspDepId")]
        public AspCourse AspCourse { get; set; }
        public int AspCourseId { get; set; }
        public int AspDepId { get; set; }
    }
}