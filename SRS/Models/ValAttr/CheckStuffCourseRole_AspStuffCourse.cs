using SRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRS.Models.ValAttr
{
    public class CheckStuffCourseRole_AspStuffCourse : ValidationAttribute
    {
        private ApplicationDbContext _context;
        public CheckStuffCourseRole_AspStuffCourse()
        {
            _context = new ApplicationDbContext();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var stuffCourse = (AspStuffCourseVM)validationContext.ObjectInstance;
            var isCourseExsist = _context.StuffCourses.Any(c => c.AspCourse.CourseId == stuffCourse.AspCourse.CourseId);
            var isUserExsist = _context.StuffCourses.Any(c => c.AspUser.UserId == stuffCourse.AspUser.UserId);
            if (isCourseExsist == true && isUserExsist == true)
            {
                return new ValidationResult("You Assign This Role before for this Stuff");
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}