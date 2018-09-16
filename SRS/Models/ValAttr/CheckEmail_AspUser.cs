using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRS.Models.ValAttr
{
    public class CheckEmail_AspUser : ValidationAttribute
    {
        private ApplicationDbContext _context;
        public CheckEmail_AspUser()
        {
            _context = new ApplicationDbContext();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var user = (AspUser)validationContext.ObjectInstance;
            var isEmailExs = _context.Users.Any(c => c.Email == user.Email);
            if (isEmailExs == true && !_context.Users.Any(c => c.UserId == user.UserId))
            {
                return new ValidationResult("Email is Alraedy Exsist");
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}