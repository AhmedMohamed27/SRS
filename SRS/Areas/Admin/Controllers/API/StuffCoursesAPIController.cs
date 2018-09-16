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
    public class StuffCoursesAPIController : ApiController
    {
        private ApplicationDbContext _contex;
        public StuffCoursesAPIController()
        {
            _contex = new ApplicationDbContext();
        }

        public IHttpActionResult GetAllStuffCourses()
        {
            var allStuuCourses = from c in _contex.StuffCourses
                                 select new AspStuffCoursesDto
                                 {
                                     Id = c.Id,
                                     FirstName = c.AspUser.FirstName,
                                     LastName = c.AspUser.LastName,
                                     Email = c.AspUser.Email,
                                     Password = c.AspUser.Password,
                                     UserId = c.AspUser.UserId,
                                     AuthKey = c.AspUser.AuthKey.ToString(),
                                     CourseId = c.AspCourse.CourseId,
                                     CourseName = c.AspCourse.CourseName,
                                     CourseTit = c.AspCourse.CourseTitle,
                                     DepId = c.AspCourse.AspDepartmentId,
                                     DepName = c.AspCourse.AspDepartment.Name
                                 };
            return Ok(allStuuCourses);
        }

        [HttpDelete]
        public void DeletestuffCourse(int id)
        {
            var stuffCourseInDb = _contex.StuffCourses.Single(c => c.Id == id);
            _contex.StuffCourses.Remove(stuffCourseInDb);
            _contex.SaveChanges();
        }
        
    }
}
