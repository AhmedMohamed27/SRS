using SRS.Models;
using SRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SRS.Areas.Admin.Controllers.API
{
    public class StudentRegisController : ApiController
    {
        private ApplicationDbContext _context;

        public StudentRegisController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Register(CourseRegistration _obj)
        {
            if (_context.Registrations.Any(c => c.UserId == _obj.UserId))
            {
                var oldRegistration = _context.Registrations.Where(c => c.UserId == _obj.UserId).ToList();
                _context.Registrations.RemoveRange(oldRegistration);
                _context.SaveChanges();

                var yearNew = DateTime.Now.Year + "/" + DateTime.Now.AddYears(1).Year;
                var coursesNew = _context.Courses.Where(c => _obj.coursesId.Contains(c.CourseId)).ToList();
                foreach (var recored in coursesNew)
                {
                    AspRegistration newRegistration = new AspRegistration();
                    newRegistration.UserId = _obj.UserId;
                    newRegistration.AspRoleId = 1;
                    newRegistration.CourseId = recored.CourseId;
                    newRegistration.AspDepartmentId = _context.Courses.Single(c => c.CourseId == recored.CourseId).AspDepartmentId;
                    newRegistration.Year = yearNew;
                    if (DateTime.Now.Month == 9 || DateTime.Now.Month == 10)
                    {
                        newRegistration.Semester = "Fall";
                    }
                    else
                    {
                        newRegistration.Semester = "Spring";
                    }

                    _context.Registrations.Add(newRegistration);
                    _context.SaveChanges();
                }
                return Ok();

            }
            var year = DateTime.Now.Year + "/" + DateTime.Now.AddYears(1).Year;
            var courses = _context.Courses.Where(c => _obj.coursesId.Contains(c.CourseId)).ToList();
            foreach (var recored in courses)
            {
                AspRegistration newRegistration = new AspRegistration();
                newRegistration.UserId = _obj.UserId;
                newRegistration.AspRoleId = 1;
                newRegistration.CourseId = recored.CourseId;
                newRegistration.AspDepartmentId = _context.Courses.Single(c => c.CourseId == recored.CourseId).AspDepartmentId;
                newRegistration.Year = year;
                if (DateTime.Now.Month == 9 || DateTime.Now.Month == 10)
                {
                    newRegistration.Semester = "Fall";
                }
                else
                {
                    newRegistration.Semester = "Spring";
                }

                _context.Registrations.Add(newRegistration);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult AllRegisForm(int userId)
        {

            var userRegistration = _context.Registrations.Where(c => c.UserId == userId)
                .Select(c => new { c.AspCourse.CourseId, c.AspCourse.CourseName, c.AspCourse.AspDepartment.Name }).ToList();
            List<RegistrationFormVm> obj = new List<RegistrationFormVm>();

            foreach (var row in userRegistration)
            {
                List<string> stuffNames = new List<string>();
                RegistrationFormVm objUsed = new RegistrationFormVm();
                objUsed.CourseId = row.CourseId;
                objUsed.CourseName = row.CourseName;
                objUsed.DepartName = row.Name;
                var stuffIds = _context.StuffCourses.Where(c => c.CourseId == row.CourseId).Select(c => new { c.UserId }).ToList();
                foreach (var nestedRow in stuffIds)
                {
                    var singleStuff = _context.Users.Single(c => c.UserId == nestedRow.UserId).FirstName + " " + _context.Users.Single(c => c.UserId == nestedRow.UserId).LastName;
                    stuffNames.Add(singleStuff);

                }
                objUsed.StuffNames = stuffNames;
                var doctorId = _context.DoctorCourses.Single(c => c.CourseId == row.CourseId).UserId;
                var doctorName = _context.Users.Single(c => c.UserId == doctorId).FirstName + " " +
                                 _context.Users.Single(c => c.UserId == doctorId).LastName;
                objUsed.DoctorNames = doctorName;
                obj.Add(objUsed);
            }
            return Ok(obj);
        }

        [HttpGet]
        public IHttpActionResult CheckPreCourses(int CourseId, int UserId)
        {
            //--Check first if this course has pre-Requist
            var isCourseHasPre = _context.PreRequisCourse.Count(c => c.CourseId == CourseId);
            if (isCourseHasPre > 0)
            {
                var isCourseRegisterd = _context.Registrations.Count(c => c.CourseId == CourseId && c.UserId == UserId);
                if (isCourseRegisterd > 0)
                {
                    return Ok();
                }

                List<string> preCoursesNameList = new List<string>();
                //-- Get the Pre-Requist Course/s id from PreCourse Table
                var preCoursesId = _context.PreRequisCourse.Where(c => c.CourseId == CourseId)
                    .Select(c => new { c.PreRquisCourseId }).ToList();
                //-- Get Course/s From Course Table
                foreach (var row in preCoursesId)
                {
                    int preCoursesIdCount = row.PreRquisCourseId;
                    if (DateTime.Now.Month == 9 || DateTime.Now.Month == 10)
                    {
                        string preCoursesNameCount =
                            _context.Courses.Single(c => c.CourseId == preCoursesIdCount).CourseName;
                        if (!_context.Courses.Any(c => c.CourseId == preCoursesIdCount && c.CourseTimeId == 2))
                        {
                            preCoursesNameList.Add(preCoursesNameCount);
                        }
                        else
                        {
                            preCoursesNameList.Add(preCoursesNameCount);
                        }
                    }
                    else
                    {
                        string preCoursesNameCount =
                            _context.Courses.Single(c => c.CourseId == preCoursesIdCount).CourseName;
                        if (_context.Courses.Any(c => c.CourseId == preCoursesIdCount && c.CourseTimeId == 1))
                        {
                            preCoursesNameList.Add(preCoursesNameCount);
                        }
                        else
                        {
                            preCoursesNameList.Add(preCoursesNameCount);
                        }
                    }

                }
                return Ok(preCoursesNameList);

            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult AvailableRegiCourese(int currentUserId)
        {
            List<int> registerCoursesIdList = new List<int>();
            List<AvailableCourseToRegis> courses = new List<AvailableCourseToRegis>();
            var isThisUserHaveRegisterCourse = _context.Registrations.Count(c => c.UserId == currentUserId);
            if (isThisUserHaveRegisterCourse > 0)
            {
                var registerCoursesId = _context.Registrations.Where(c => c.UserId == currentUserId)
                    .Select(c => new { c.CourseId }).ToList();
                foreach (var row in registerCoursesId)
                {
                    int temp = row.CourseId;
                    registerCoursesIdList.Add(temp);
                }
                var returnedCourses = _context.Courses.Where(c => !registerCoursesIdList.Any(c2 => c2 == c.CourseId)).Select(c => new { c.CourseId }).ToList();
                foreach (var row in returnedCourses)
                {
                    AvailableCourseToRegis _obj = new AvailableCourseToRegis();
                    int temp = row.CourseId;

                    string returedCourse;
                    if (DateTime.Now.Month == 9 || DateTime.Now.Month == 10)
                    {
                        if (_context.Courses.Any(c => c.CourseId == temp && c.CourseTimeId == 2))
                        {
                            var hasPreReq = _context.PreRequisCourse.Any(c => c.CourseId == temp);
                            if (hasPreReq)
                            {
                                var preCourseId = _context.PreRequisCourse.Single(c => c.CourseId == temp)
                                    .PreRquisCourseId;
                                if (_context.Registrations.Any(c =>
                                    c.UserId == currentUserId && c.CourseId == preCourseId))
                                {
                                    returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 2).CourseName;
                                    _obj.CourseName = returedCourse;
                                    _obj.CourseId = currentUserId;
                                    courses.Add(_obj);
                                }
                            }
                            else
                            {
                                returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 2).CourseName;
                                _obj.CourseName = returedCourse;
                                _obj.CourseId = currentUserId;
                                courses.Add(_obj);
                            }

                        }
                    }
                    else
                    {
                        if (_context.Courses.Any(c => c.CourseId == temp && c.CourseTimeId == 1))
                        {
                            var hasPreReq = _context.PreRequisCourse.Any(c => c.CourseId == temp);
                            if (hasPreReq)
                            {
                                var preCourseId = _context.PreRequisCourse.Single(c => c.CourseId == temp)
                                    .PreRquisCourseId;
                                if (_context.Registrations.Any(c =>
                                    c.UserId == currentUserId && c.CourseId == preCourseId))
                                {
                                    returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 1).CourseName;
                                    _obj.CourseName = returedCourse;
                                    _obj.CourseId = currentUserId;
                                    courses.Add(_obj);
                                }
                            }
                            else
                            {
                                returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 1).CourseName;
                                _obj.CourseName = returedCourse;
                                _obj.CourseId = currentUserId;
                                courses.Add(_obj);
                            }
                        }

                    }


                }
                return Ok(courses);
            }
            var returnedCourses2 = _context.Courses.Select(c => new { c.CourseId }).ToList();
            foreach (var row in returnedCourses2)
            {
                AvailableCourseToRegis _obj = new AvailableCourseToRegis();
                int temp = row.CourseId;

                string returedCourse;
                if (DateTime.Now.Month == 9 || DateTime.Now.Month == 10)
                {
                    if (_context.Courses.Any(c => c.CourseId == temp && c.CourseTimeId == 2))
                    {
                        var hasPreReq = _context.PreRequisCourse.Any(c => c.CourseId == temp);
                        if (hasPreReq)
                        {
                            var preCourseId = _context.PreRequisCourse.Single(c => c.CourseId == temp)
                                .PreRquisCourseId;
                            if (_context.Registrations.Any(c =>
                                c.UserId == currentUserId && c.CourseId == preCourseId))
                            {
                                returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 2).CourseName;
                                _obj.CourseName = returedCourse;
                                _obj.CourseId = currentUserId;
                                courses.Add(_obj);
                            }
                        }
                        else
                        {
                            returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 2).CourseName;
                            _obj.CourseName = returedCourse;
                            _obj.CourseId = currentUserId;
                            courses.Add(_obj);
                        }

                    }
                }
                else
                {
                    if (_context.Courses.Any(c => c.CourseId == temp && c.CourseTimeId == 1))
                    {
                        var hasPreReq = _context.PreRequisCourse.Any(c => c.CourseId == temp);
                        if (hasPreReq)
                        {
                            var preCourseId = _context.PreRequisCourse.Single(c => c.CourseId == temp)
                                .PreRquisCourseId;
                            if (_context.Registrations.Any(c =>
                                c.UserId == currentUserId && c.CourseId == preCourseId))
                            {
                                returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 1).CourseName;
                                _obj.CourseName = returedCourse;
                                _obj.CourseId = currentUserId;
                                courses.Add(_obj);
                            }
                        }
                        else
                        {
                            returedCourse = _context.Courses.Single(c => c.CourseId == temp && c.CourseTimeId == 1).CourseName;
                            _obj.CourseName = returedCourse;
                            _obj.CourseId = currentUserId;
                            courses.Add(_obj);
                        }
                    }

                }


            }
            return Ok(courses);
        }


        [HttpGet]
        public IHttpActionResult TimeForCourse(int CourseId)
        {
            bool checkTiem = new bool();
            var courseInDb = _context.Courses.Single(c => c.CourseId == CourseId).CourseTimeId;
            if (DateTime.Now.Month == 9 || DateTime.Now.Month == 10)
            {
                if (courseInDb == 2)
                {
                    checkTiem = true;
                }
                else
                {
                    checkTiem = false;
                }
            }
            else
            {
                if (courseInDb == 1)
                {
                    checkTiem = true;
                }
                else
                {
                    checkTiem = false;
                }
            }

            return Ok(checkTiem);

        }
    }
}
