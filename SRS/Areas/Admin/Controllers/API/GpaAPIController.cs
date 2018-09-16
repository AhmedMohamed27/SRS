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
    public class GpaAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public GpaAPIController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult InsertDefGpaVal(int userId)
        {
            if(_context.Gpa.Any(c=>c.UserId == userId))
            {
                return Ok();
            }
            else
            {
                var userInDb = _context.Users.Single(c => c.UserId == userId);
                AspGPA newRec = new AspGPA();
                newRec.GpaVal = 0;
                newRec.Pendinghoures = 141;
                newRec.Takenhoures = 0;
                newRec.UserId = userId;
                newRec.RoleId = userInDb.AspRoleId;
                _context.Gpa.Add(newRec);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
