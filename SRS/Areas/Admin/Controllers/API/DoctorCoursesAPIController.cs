using SRS.Models;
using SRS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SRS.Areas.Admin.Controllers.API
{
    public class DoctorCoursesAPIController : ApiController
    {
        private ApplicationDbContext _contex;
        public DoctorCoursesAPIController()
        {
            _contex = new ApplicationDbContext();
        }

        public IHttpActionResult GetAllStuffCourses()
        {
            var allStuuCourses = from c in _contex.DoctorCourses
                                 select new AspStuffCoursesDto
                                 {
                                     Id = c.Id,
                                     FirstName = c.User.FirstName,
                                     LastName = c.User.LastName,
                                     Email = c.User.Email,
                                     Password = c.User.Password,
                                     UserId = c.User.UserId,
                                     AuthKey = c.User.AuthKey.ToString(),
                                     CourseId = c.Course.CourseId,
                                     CourseName = c.Course.CourseName,
                                     CourseTit = c.Course.CourseTitle,
                                     DepId = c.Course.AspDepartmentId,
                                     DepName = c.Course.AspDepartment.Name
                                 };
            return Ok(allStuuCourses);
        }

        [HttpDelete]
        public void DeletestuffCourse(int id)
        {
            var stuffCourseInDb = _contex.DoctorCourses.Single(c => c.Id == id);
            _contex.DoctorCourses.Remove(stuffCourseInDb);
            _contex.SaveChanges();
        }
    }
}
