using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.ViewModel
{
    public class RegistrationFormVm
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public string DepartName { get; set; }
        public List<string> StuffNames { get; set; }
        public string DoctorNames { get; set; }
    }
}