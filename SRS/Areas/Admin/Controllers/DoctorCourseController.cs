using SRS.Models;
using SRS.Models.ViewModel;
using SRS.Models.ValAttr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Areas.Stuff.Controllers
{
    public class DoctorCourseController : Controller
    {
        

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

        private ApplicationDbContext _context;
        public DoctorCourseController()
        {
            _context = new ApplicationDbContext();
        }
        
        // GET: Stuff/Course
        public ActionResult Index()
        {
            
            if (CheckCookies() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }
        }

        public ActionResult Add()
        {

            if (CheckCookies() == true)
            {
                var viewModel = new AspStuffCourseVM
                {
                    AspCourse = new AspCourse(),
                    CourseDrobDown = _context.Courses.ToList(),
                    AspUser = new AspUser(),
                    UserDrobDown = _context.Users.ToList()
                };
                return View("DoctorCourseForm", viewModel);
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }
        }

        [HttpPost]
        public ActionResult Save(AspStuffCourseVM stuffCourseVM)
        {

            if (CheckCookies() == true)
            {
                var firstname = stuffCourseVM.AspUser.FirstName = _context.Users.Single(c => c.UserId == stuffCourseVM.AspUser.UserId).FirstName;
                var lastname = stuffCourseVM.AspUser.LastName = _context.Users.Single(c => c.UserId == stuffCourseVM.AspUser.UserId).LastName;
                var userId = stuffCourseVM.AspUser.UserId = _context.Users.Single(c => c.UserId == stuffCourseVM.AspUser.UserId).UserId;
                var roleId = stuffCourseVM.AspUser.AspRoleId = _context.Users.Single(c => c.UserId == stuffCourseVM.AspUser.UserId).AspRoleId;
                var email = stuffCourseVM.AspUser.Email = _context.Users.Single(c => c.UserId == stuffCourseVM.AspUser.UserId).Email;
                var authkey = stuffCourseVM.AspUser.AuthKey = _context.Users.Single(c => c.UserId == stuffCourseVM.AspUser.UserId).AuthKey;
                var password = stuffCourseVM.AspUser.Password = _context.Users.Single(c => c.UserId == stuffCourseVM.AspUser.UserId).Password;
                // ------------------------------------------------------------------------------------------------------------------------------------
                var coursename = stuffCourseVM.AspCourse.CourseName = _context.Courses.Single(c => c.CourseId == stuffCourseVM.AspCourse.CourseId).CourseName;
                var coursetit = stuffCourseVM.AspCourse.CourseTitle = _context.Courses.Single(c => c.CourseId == stuffCourseVM.AspCourse.CourseId).CourseTitle;
                var courseid = stuffCourseVM.AspCourse.CourseId = _context.Courses.Single(c => c.CourseId == stuffCourseVM.AspCourse.CourseId).CourseId;
                var depid = stuffCourseVM.AspCourse.AspDepartmentId = _context.Courses.Single(c => c.CourseId == stuffCourseVM.AspCourse.CourseId).AspDepartmentId;
                AspDoctorCourses newStuffCourses = new AspDoctorCourses
                {
                    CourseName = coursename,
                    CourseTit = coursetit,
                    CourseId = courseid,
                    DepId = depid,
                    FirstName = firstname,
                    LastName = lastname,
                    UserId = userId,
                    RoleId = roleId,
                    Email = email,
                    AuthKey = authkey.ToString(),
                    Password = password,

                };
                if (_context.DoctorCourses.Any(c => c.UserId == stuffCourseVM.AspUser.UserId) && _context.DoctorCourses.Any(c => c.CourseId == stuffCourseVM.AspCourse.CourseId))
                {
                    return RedirectToAction("Index");
                }
                _context.DoctorCourses.Add(newStuffCourses);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }

        }
    }
}