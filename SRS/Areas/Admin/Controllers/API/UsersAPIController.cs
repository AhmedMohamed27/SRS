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
    public class UsersAPIController : ApiController
    {
        private ApplicationDbContext _context;
        public UsersAPIController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetAllUsers()
        {
            var users = from c in _context.Users
                        where c.AspRoleId == 4
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
        

        [HttpDelete]
        public void Delete(int id)
        {
            var userInDb = _context.Users.Single(c => c.UserId == id);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
        }


        

    }
}
