using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SRS.Areas.Stuff.Controllers.API
{
    public class RolesAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public RolesAPIController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetAllRoles()
        {
            var rolesInDb = _context.Roles.ToList();
            return Ok(rolesInDb);
        }

        [HttpDelete]
        public void DeleteRole(int id)
        {
            var roleInDb = _context.Roles.Single(c => c.RoleId == id);
            _context.Roles.Remove(roleInDb);
            _context.SaveChanges();
        }
    }
}
