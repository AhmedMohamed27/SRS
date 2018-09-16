using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Areas.Student.Controllers
{
    public class StudentUserController : Controller
    {
        private ApplicationDbContext _context;
        public StudentUserController()
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
                    ViewData["isRegisterBefore"] = _context.Registrations.Count(c => c.UserId == user.UserId);
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
        // GET: Student/User
        public ActionResult Index()
        {
            if (CheckCookies() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "User",new {area="Admin" });
            }
        }
    }
}