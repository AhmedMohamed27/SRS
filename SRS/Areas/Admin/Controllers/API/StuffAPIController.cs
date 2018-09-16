using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SRS.Areas.Stuff.Controllers.API
{
    public class StuffAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public StuffAPIController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetStuffs()
        {
            var users = from c in _context.Users
                        where c.AspRoleId == 3
                        select new AspUserDto
                        {
                            UserId = c.UserId,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            Phone = c.Phone,
                            AspRoleName = c.AspRole.Name,
                            StudentID = c.StudentID,
                            StudentLvlName = c.AspStudentLvl.lvlName

                        };
            return Ok(users);
        }
    }
}
