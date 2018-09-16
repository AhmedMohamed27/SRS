using System.Collections.Generic;

namespace SRS.Models.ViewModel
{
    public class AspCourseVM
    {

        public AspCourse AspCourse { get; set; }

        public IEnumerable<AspDepartment> AspDepartment { get; set; }

        public IEnumerable<AspCourseTime> AspCourseTime { get; set; }
    }
}