using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.Dto
{
    public class AspPreRequistDto
    {
        public int Id { get; set; }
        public AspCourse Course { get; set; }
        public int CourseId { get; set; }
        // ------------------------------------------------
        
        public int CourseDepId { get; set; }
        // ------------------------------------------------
        
        public string CourseName { get; set; }
        // ------------------------------------------------
        
        public string CourseTit { get; set; }
        // ------------------------------------------------
        
        public AspCourse PreRquisCourse { get; set; }
        // ------------------------------------------------

        public int PreRquisCourseId { get; set; }
        // ------------------------------------------------
        
        public int PreRquisCourseDepId { get; set; }
        // ------------------------------------------------
        
        public string PreCourseName { get; set; }
        // ------------------------------------------------
        
        public string PreCourseTit { get; set; }

    }
}