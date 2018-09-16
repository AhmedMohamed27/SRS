using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    public class AspUserDto
    {
        
        public int UserId { get; set; }

        public string AspRoleName { get; set; }

        public string FirstName { get; set; }

        
        public string LastName { get; set; }

       
        public string Email { get; set; }

        
        public string Password { get; set; }

        
        public string Phone { get; set; }

        public string StudentLvlName { get; set; }

        public string StudentID { get; set; }
    }
}