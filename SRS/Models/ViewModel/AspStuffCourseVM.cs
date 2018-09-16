using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SRS.Models;
using SRS.Models.ValAttr;

namespace SRS.Models.ViewModel
{
    
    public class AspStuffCourseVM
    {
        
        public IEnumerable<AspUser> UserDrobDown { get; set; }
        public AspUser AspUser { get; set; }
        

        public IEnumerable<AspCourse> CourseDrobDown { get; set; }
        public AspCourse AspCourse { get; set; }

       



    }
}