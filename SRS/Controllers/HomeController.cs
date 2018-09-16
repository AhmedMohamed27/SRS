using SRS.Models;
using SRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            var viewModel = new LoginVM();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginVM vm)
        {
            try
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                if (!ModelState.IsValid)
                {
                    var viewModel = new LoginVM
                    {
                        Email = vm.Email,
                        Password = vm.Password
                    };
                    return View(viewModel);
                }
                else
                {
                    var userIndb = _context.Users.Single(c => c.Email == vm.Email);
                    var userId = userIndb.UserId;
                    var AuthKey = userIndb.AuthKey;
                    var userId_AuthKey = userId + "||" + AuthKey;
                    var Enc = Crypto.encrypt(userId_AuthKey);
                    var isRegisterd = _context.Registrations.Count(c => c.UserId == userId);
                    HttpCookie cookie = new HttpCookie("User");
                    cookie.Values.Add("UserID", userIndb.UserId.ToString());
                    cookie.Values.Add("FirstName", userIndb.FirstName);
                    cookie.Values.Add("LastName", userIndb.LastName);
                    cookie.Values.Add("Email", userIndb.Email);
                    cookie.Values.Add("Role", userIndb.AspRoleId.ToString());
                    cookie.Values.Add("Level", userIndb.AspStudentLvlId.ToString());
                    cookie.Values.Add("Enc", Enc);
                    cookie.Values.Add("isRegisterBefore", isRegisterd.ToString());
                    Response.Cookies.Add(cookie);
                    if(userIndb.AspRoleId == 4)
                    {
                        return RedirectToAction("Index", "User", new { area = "Admin" });
                    }
                    else if(userIndb.AspRoleId == 3)
                    {
                        return RedirectToAction("Index", "Courses", new { area = "Stuff" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "StudentUser", new { area = "Student" });
                    }
                }

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var dbValidationError in entityValidationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("PropertyName: {0} ErrorMessage: {1}", dbValidationError.PropertyName, dbValidationError.ErrorMessage);
                    }
                }
                return Content(ex.Message);
            }
        }
    }
}