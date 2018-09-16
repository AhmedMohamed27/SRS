using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRS.Models.ValAttr
{
    public class CheckStudentId_AspUser : ValidationAttribute
    {
        private ApplicationDbContext _context;
        public CheckStudentId_AspUser()
        {
            _context = new ApplicationDbContext();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var user = (AspUser)validationContext.ObjectInstance;
            var isStudentIDExs = _context.Users.Any(c => c.StudentID == user.StudentID);
            if (isStudentIDExs == true && !_context.Users.Any(c => c.UserId == user.UserId))
            {
                var isEmailNull = _context.Users.Any(c => c.StudentID == "" || c.StudentID == null);
                if (isEmailNull == true)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Student Id is Alraedy Exsist");
                }
                
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}