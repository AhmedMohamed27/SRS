using SRS.Models;
using SRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Areas.Stuff.Controllers
{
    public class PreRequisCourseController : Controller
    {
        private ApplicationDbContext _context;
        public PreRequisCourseController()
        {
            _context = new ApplicationDbContext();
        }
        public bool CheckCookies()
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                string InputEnc = Request.Cookies["User"]["Enc"];
                string ouputEnc = Crypto.Decrypt(InputEnc.ToString());
                string[] Key = ouputEnc.Split(new string[] { "||" }, StringSplitOptions.None);
                string KeyId = Key[0];
                string KeyAuth = Key[1];
                int Usercount = _context.Users.Where(c => c.UserId.ToString() == KeyId && c.AuthKey.ToString() == KeyAuth).Count();
                if (Usercount == 1)
                {

                    var user = _context.Users.Single(c => c.UserId.ToString() == KeyId);
                    ViewData["Id"] = user.UserId;
                    ViewData["FirstName"] = user.FirstName;
                    ViewData["LastName"] = user.LastName;
                    ViewData["FullName"] = user.FirstName + " " + user.LastName;
                    ViewData["Email"] = user.Email;
                    ViewData["Role"] = user.AspRoleId.ToString();
                    ViewData["Level"] = user.AspStudentLvlId.ToString();
                    ViewData["Key"] = InputEnc;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        // GET: Stuff/PreRequisCourse
        public ActionResult Index()
        {
            if (CheckCookies() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout","User");
            }
    }

        public ActionResult Assign()
        {
            if (CheckCookies() == true)
            {
                var viewModel = new AspPreRequisCourseVM
                {
                    _Course = new AspCourse(),
                    Course = _context.Courses.ToList(),
                    _PreRquisCourse = new AspCourse(),
                    PreRquisCourse = _context.Courses.ToList()
                };
                return View("PreCourseform", viewModel);
            }
            else
            {
                return RedirectToAction("Logout","User");
            }
            
        }

        public ActionResult Save(AspPreRequisCourseVM preRequiscourseVM)
        {
            try
            {
                if (CheckCookies() == true)
                {
                    if (_context.PreRequisCourse.Any(c => c.CourseId == preRequiscourseVM._Course.CourseId && c.PreRquisCourseId == preRequiscourseVM._PreRquisCourse.CourseId) == false)
                    {
                        AspPreRequisCourse preCourse = new AspPreRequisCourse
                        {
                            CourseName = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._Course.CourseId).CourseName,
                            CourseTit = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._Course.CourseId).CourseTitle,
                            CourseId = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._Course.CourseId).CourseId,
                            CourseDepId = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._Course.CourseId).AspDepartmentId,

                            PreCourseName = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._PreRquisCourse.CourseId).CourseName,
                            PreCourseTit = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._PreRquisCourse.CourseId).CourseTitle,
                            PreRquisCourseId = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._PreRquisCourse.CourseId).CourseId,
                            PreRquisCourseDepId = _context.Courses.Single(c => c.CourseId == preRequiscourseVM._PreRquisCourse.CourseId).AspDepartmentId,
                        };
                        _context.PreRequisCourse.Add(preCourse);
                    }
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Logout", "User");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
    }
}