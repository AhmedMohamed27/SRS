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
    public class PreRequisCourseAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public PreRequisCourseAPIController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetCoursesPreRequist()
        {
            var allPreReq = from c in _context.PreRequisCourse
                            select new AspPreRequistDto
                            {
                                Id = c.id,
                                CourseName = c.Course.CourseName,
                                PreCourseName = c.PreRquisCourse.CourseName
                            };
            return Ok(allPreReq);
        }

        [HttpDelete]
        public void DeletePreRequist(int id)
        {
            var recoredInDb = _context.PreRequisCourse.Single(c => c.id == id);
            _context.PreRequisCourse.Remove(recoredInDb);
            _context.SaveChanges();
        }
    }
}
