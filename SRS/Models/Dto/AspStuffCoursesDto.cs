using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models.Dto
{
    public class AspStuffCoursesDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }

        public string AuthKey { get; set; } 

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseTit { get; set; }

        public int DepId { get; set; }

        public string DepName { get; set; }
    }
}