using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Areas.Admin
{
    public class DoctorCoursesController : Controller
    {
        // GET: Admin/DoctorCourses
        public ActionResult Index()
        {
            return View();
        }
    }
}