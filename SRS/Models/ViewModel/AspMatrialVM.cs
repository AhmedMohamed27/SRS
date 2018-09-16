using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.ViewModel
{
    public class AspMatrialVM
    {
        public IEnumerable<string> Fname { get; set; }
        public IEnumerable<AspCourse> AspCourse { get; set; }

        public AspMatrial AspMatrial { get; set; }
    }
}