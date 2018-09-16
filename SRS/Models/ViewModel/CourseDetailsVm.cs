using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.ViewModel
{
    public class CourseDetailsVm
    {
        public int CourseId { get; set; }
        public string Coursename { get; set; }
        public string CourseDepartment { get; set; }
        public string CourseTime { get; set; }
        public string CourseCode { get; set; }
        public List<string> CourseStuffs { get; set; }
        public string CourseDoctor { get; set; }
        public string CourseCredit { get; set; }

        public List<StudentCourseDetVm> students { get; set; }
    }
}