using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.ViewModel
{
    public class AspPreRequisCourseVM
    {
        
        public ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<AspCourse> Course { get; set; }

        public AspCourse _Course { get; set; }
        
        

        public IEnumerable<AspCourse> PreRquisCourse { get; set; }

        public AspCourse _PreRquisCourse { get; set; }

        
    }
}