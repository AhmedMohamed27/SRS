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
    public class AspStuffCourseAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public AspStuffCourseAPIController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetStuffCourses(int UserId)
        {
            List<AspStuffCourses> obj = new List<AspStuffCourses>();
            if (_context.StuffCourses.Any())
            {
                var allStuffCourses = from c in _context.StuffCourses
                                      where c.UserId == UserId
                                      select new AspStuffCoursesDto
                                      {
                                          Id = c.Id,
                                          CourseId = c.CourseId,
                                          CourseName = c.AspCourse.CourseName,
                                          CourseTit = c.AspCourse.CourseTitle,
                                          DepId = c.DepId,
                                          DepName = c.AspCourse.AspDepartment.Name
                                      };
                foreach(var row in allStuffCourses)
                {
                    AspStuffCourses _obj = new AspStuffCourses();
                    _obj.Id = row.Id;
                    _obj.CourseId = row.CourseId;
                    _obj.CourseName = row.CourseName;
                    _obj.CourseTit = row.CourseTit;
                    _obj.DepId = row.DepId;
                    _obj.DeptName = row.DepName;
                    obj.Add(_obj);
                }
                return Json(obj);
            }
            else
            {
                return Json(obj);
            }
            
        }
    }
}
