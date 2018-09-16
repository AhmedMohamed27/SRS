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
    public class AllCoursesAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public AllCoursesAPIController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<AspCourseDto> GetCourses(string query = null)
        {
            var courses = from c in _context.Courses
                          select new AspCourseDto
                          {
                              CourseId = c.CourseId,
                              CourseName = c.CourseName,
                              DeparetmentName = c.AspDepartment.Name,
                              CourseTitle = c.CourseTitle,
                              CreadedHours = c.CreadedHours
                          };
            if (!String.IsNullOrWhiteSpace(query))
                courses = courses.Where(c => c.CourseName.Contains(query));
            return courses;
        }

        [HttpGet]
        public IHttpActionResult GetUserInfo(int userId)
        {
            
            var userInDb = _context.Users.Single(c => c.UserId == userId);
            var userGpa = _context.Gpa.Where(c => c.UserId == userId).Single();
            AspCourseDto obj = new AspCourseDto()
            {
                UserFullName = userInDb.FirstName + " " + userInDb.LastName,
                GpaVal = (float) userGpa.GpaVal,
                Pendinghoures = userGpa.Pendinghoures,
                Takenhours = userGpa.Takenhoures
            };
            return Json(obj);
        }

        [HttpGet]
        public IHttpActionResult GetStuffInfo(int CourseId)
        {
            List<AspCourseStuffInfoDto> obj = new List<AspCourseStuffInfoDto>();
            if (!_context.StuffCourses.Any(c=>c.CourseId == CourseId))
            {
                return Json(obj);
            }
            else if (!_context.DoctorCourses.Any(c => c.CourseId == CourseId))
            {
                return Json(obj);
            }
            else
            {
                var StuffInfo = _context.StuffCourses.Where(c => c.CourseId == CourseId).Select(c => new { c.AspUser.FirstName, c.AspUser.LastName }).ToList();
                var DoctorInfo = _context.DoctorCourses.Where(c => c.CourseId == CourseId).Select(c => new { c.User.FirstName, c.User.LastName }).ToList();

                foreach (var stuff in StuffInfo)
                {
                    AspCourseStuffInfoDto _obj1 = new AspCourseStuffInfoDto();
                    _obj1.StuffName = stuff.FirstName + " " + stuff.LastName;
                    _obj1.DoctorName = "";
                    obj.Add(_obj1);
                }
                foreach (var doctor in DoctorInfo)
                {
                    AspCourseStuffInfoDto _obj2 = new AspCourseStuffInfoDto();
                    _obj2.DoctorName = doctor.FirstName + " " + doctor.LastName;
                    _obj2.StuffName = "";
                    obj.Add(_obj2);
                }
                return Json(obj);
            }
            
        }
    }
}
