using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SRS.Areas.Stuff.Controllers.API
{
    public class MatrialAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public MatrialAPIController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetAllMat()
        {
            var matInDb = from c in _context.Matrials
                          select new AspMatrialDto
                          {
                              Id = c.Id,
                              Desc = c.Desc,
                              FilePath = c.FilePath,
                              AspCourseName = c.AspCourse.CourseName,
                              AspDepName = c.AspCourse.AspDepartment.Name
                          };
            return Ok(matInDb);
        }
    }
}
