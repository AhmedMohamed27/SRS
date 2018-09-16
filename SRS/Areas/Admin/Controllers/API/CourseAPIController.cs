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
    public class CourseAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public CourseAPIController()
        {
            _context = new ApplicationDbContext();
        }
        
        public IHttpActionResult GetGeneralCourses()
        {
            var courses = from c in _context.Courses
                          where c.AspDepartment.Name == "General"
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

        
        [HttpDelete]
        public void DeleteCourse(int id)
        {
            var courseInDb = _context.Courses.Single(c => c.CourseId == id);
            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
        }
    }
}
