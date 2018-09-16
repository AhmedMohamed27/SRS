using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.ViewModel
{
    public class AspRegistrationVM
    {
        public AspRegistration Registration { get; set; }
        public IEnumerable<AspCourse> Courses { get; set; }
    }
}