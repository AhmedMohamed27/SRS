using SRS.Models;
using SRS.Models.ViewModel;
using SRS.Models.ValAttr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace SRS.Areas.Stuff.Controllers
{
    public class CourseController : Controller
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
        public CourseController()
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
                var viewModel = new AspCourseVM
                {
                    AspCourse = new AspCourse(),
                    AspDepartment = _context.Departments.ToList(),
                    AspCourseTime = _context.CourseTimes.ToList()
                };
                return View("CourseForm", viewModel);
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }
        }

        public ActionResult Update(int id)
        {

            if (CheckCookies() == true)
            {
                var courseInDb = _context.Courses.Single(c => c.CourseId == id);
                var viewModel = new AspCourseVM
                {
                    AspCourse = courseInDb,
                    AspDepartment = _context.Departments.ToList(),
                    AspCourseTime = _context.CourseTimes.ToList()
                };
                return View("CourseForm", viewModel);
            }
            return RedirectToAction("Logout", "User");

        }

        public ActionResult Details(int id)
        {
            if (CheckCookies())
            {
                
                Session["ID"] = id;
                var courseInDb = _context.Courses.Single(c => c.CourseId == id);
                int doctorCourseId;
                string doctorName ="";
                if (_context.DoctorCourses.Any(x => x.CourseId == courseInDb.CourseId))
                {
                    doctorCourseId = _context.DoctorCourses.Single(c => c.CourseId == courseInDb.CourseId).UserId;
                    doctorName = _context.Users.Single(c => c.UserId == doctorCourseId).FirstName + " " +
                                 _context.Users.Single(c => c.UserId == doctorCourseId).LastName;
                }
                else
                {
                    doctorName = "No Doctor Assiged For this Course Until Now";
                }
                List<string> stuffNames = new List<string>();
                List<StudentCourseDetVm> obj = new List<StudentCourseDetVm>();
                if (_context.StuffCourses.Any(c => c.CourseId == courseInDb.CourseId))
                {
                    var stuffIds = _context.StuffCourses.Where(c => c.CourseId == courseInDb.CourseId)
                        .Select(c => new { c.UserId }).ToList();

                    foreach (var stuff in stuffIds)
                    {
                        string temp = "";
                        temp = _context.Users.Single(c => c.UserId == stuff.UserId).FirstName + " " +
                               _context.Users.Single(c => c.UserId == stuff.UserId).LastName;
                        stuffNames.Add(temp);
                    }
                }
                if ( _context.Registrations.Any(z=>z.CourseId == courseInDb.CourseId))
                {
                    var studentIds = _context.Registrations.Where(c => c.CourseId == courseInDb.CourseId)
                        .Select(c => new { c.UserId }).ToList();
                    
                    foreach (var student in studentIds)
                    {
                        StudentCourseDetVm tempObj = new StudentCourseDetVm();
                        string studentId = _context.Users.Single(c => c.UserId == student.UserId).StudentID;
                        string name = _context.Users.Single(c => c.UserId == student.UserId).FirstName + " " +
                                      _context.Users.Single(c => c.UserId == student.UserId).LastName;
                        string email = _context.Users.Single(c => c.UserId == student.UserId).Email;
                        var levelId = _context.Users.Single(c => c.UserId == student.UserId).AspStudentLvlId;
                        string level = _context.StudentLvl.Single(c => c.Id == levelId).lvlName;
                        tempObj.Id = studentId;
                        tempObj.Email = email;
                        tempObj.Name = name;
                        tempObj.Level = level;
                        obj.Add(tempObj);
                    }
                }
                var viewModel = new CourseDetailsVm
                {
                    CourseId = courseInDb.CourseId,
                    Coursename = courseInDb.CourseName,
                    CourseDepartment = _context.Departments.Single(c => c.DepId == courseInDb.AspDepartmentId).Name,
                    CourseCode = courseInDb.CourseTitle,
                    CourseCredit = "3",
                    CourseTime = _context.CourseTimes.Single(c => c.Id == courseInDb.CourseTimeId).Time,
                    CourseDoctor = doctorName,
                    CourseStuffs = stuffNames,
                    students = obj
                };
                return View(viewModel);
            }
            return RedirectToAction("Logout", "User");
        }

        public ActionResult PrintPartialViewToPdf(int id)
        {
            if (CheckCookies())
            {
                var courseInDb = _context.Courses.Single(c => c.CourseId == id);
                var doctorCourseId = _context.DoctorCourses.Single(c => c.CourseId == courseInDb.CourseId).UserId;
                var stuffIds = _context.StuffCourses.Where(c => c.CourseId == courseInDb.CourseId)
                    .Select(c => new { c.UserId }).ToList();
                List<string> stuffNames = new List<string>();
                foreach (var stuff in stuffIds)
                {
                    string temp = "";
                    temp = _context.Users.Single(c => c.UserId == stuff.UserId).FirstName + " " +
                           _context.Users.Single(c => c.UserId == stuff.UserId).LastName;
                    stuffNames.Add(temp);
                }

                var studentIds = _context.Registrations.Where(c => c.CourseId == courseInDb.CourseId)
                    .Select(c => new { c.UserId }).ToList();
                List<StudentCourseDetVm> obj = new List<StudentCourseDetVm>();
                foreach (var student in studentIds)
                {
                    StudentCourseDetVm tempObj = new StudentCourseDetVm();
                    string studentId = _context.Users.Single(c => c.UserId == student.UserId).StudentID;
                    string name = _context.Users.Single(c => c.UserId == student.UserId).FirstName + " " +
                                  _context.Users.Single(c => c.UserId == student.UserId).LastName;
                    string email = _context.Users.Single(c => c.UserId == student.UserId).Email;
                    var levelId = _context.Users.Single(c => c.UserId == student.UserId).AspStudentLvlId;
                    string level = _context.StudentLvl.Single(c => c.Id == levelId).lvlName;
                    tempObj.Id = studentId;
                    tempObj.Email = email;
                    tempObj.Name = name;
                    tempObj.Level = level;
                    obj.Add(tempObj);
                }

                CourseDetailsVm model = new CourseDetailsVm
                {
                    CourseId = courseInDb.CourseId,
                    Coursename = courseInDb.CourseName,
                    CourseDepartment = _context.Departments.Single(c => c.DepId == courseInDb.AspDepartmentId).Name,
                    CourseCode = courseInDb.CourseTitle,
                    CourseCredit = "3",
                    CourseTime = _context.CourseTimes.Single(c => c.Id == courseInDb.CourseTimeId).Time,
                    CourseDoctor = _context.Users.Single(c => c.UserId == doctorCourseId).FirstName + " " +
                                   _context.Users.Single(c => c.UserId == doctorCourseId).LastName,
                    CourseStuffs = stuffNames,
                    students = obj
                };

                var report = new PartialViewAsPdf("~/Views/Shared/_CourseResult.cshtml", model);
                return report;
            }
            return RedirectToAction("Logout", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AspCourseVM course)
        {

            if (CheckCookies() == true)
            {
                AspCourse _course = new AspCourse
                {
                    CourseId = course.AspCourse.CourseId,
                    CourseName = course.AspCourse.CourseName,
                    AspDepartmentId = course.AspCourse.AspDepartmentId,
                    CourseTitle = course.AspCourse.CourseTitle,
                    CreadedHours = course.AspCourse.CreadedHours,
                    CourseTimeId = course.AspCourse.CourseTimeId,
                };
                if (!ModelState.IsValid)
                {
                    var viewModel = new AspCourseVM
                    {
                        AspCourse = _course,
                        AspDepartment = _context.Departments.ToList(),
                        AspCourseTime = _context.CourseTimes.ToList()
                    };
                    return View("CourseForm", viewModel);
                }

                if (course.AspCourse.CourseId == 0)
                {
                    _context.Courses.Add(_course);
                }
                else
                {
                    var courseInDb = _context.Courses.Single(c => c.CourseId == _course.CourseId);
                    //_context.Users.Remove(userInDb);
                    //_context.SaveChanges();
                    courseInDb.CourseName = _course.CourseName;
                    courseInDb.AspDepartmentId = _course.AspDepartmentId;
                    courseInDb.CourseTitle = _course.CourseTitle;
                    courseInDb.CreadedHours = _course.CreadedHours;
                    courseInDb.CourseTimeId = _course.CourseTimeId;
                }
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