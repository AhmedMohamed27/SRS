using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.ViewModel
{
    public class CourseRegistration
    {
        public List<int> coursesId { get; set; }

        public int UserId { get; set; }
    }
}