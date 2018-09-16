using SRS.Models;
using SRS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SRS.Areas.Stuff.Controllers.API
{
    public class SweAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public SweAPIController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetSWECourses()
        {
            var courses = from c in _context.Courses
                          where c.AspDepartment.Name == "SWE"
                          select new AspCourseDto
                          {
                              CourseId = c.CourseId,
                              CourseName = c.CourseName,
                              DeparetmentName = c.AspDepartment.Name,
                              CourseTitle = c.CourseTitle,
                              CreadedHours = c.CreadedHours
                          };
            return Ok(courses);
        }
    }
}
