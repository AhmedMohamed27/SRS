using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.Dto
{
    public class AspCourseDto
    {

        public int CourseId { get; set; }

        public string DeparetmentName { get; set; }
        
        public string CourseName { get; set; }
        
        public string CourseTitle { get; set; }

        public int CreadedHours { get; set; }

        public int Takenhours { get; set; }

        public int Pendinghoures { get; set; }

        public float GpaVal { get; set; }

        public string UserFullName { get; set; }

        public string StuffName { get; set; }

        public string DoctorName { get; set; }
    }
}