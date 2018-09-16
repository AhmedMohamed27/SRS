using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Areas.Stuff.Controllers
{
    public class RolesController : Controller
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
        public RolesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Stuff/Roles
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

        public ActionResult New()
        {
            if (CheckCookies() == true)
            {
                var viewMode = new AspRole();
                return View("RoleForm", viewMode);
            }
            else
            {
                return RedirectToAction("Logout","User");
            }
            
        }

        public ActionResult Update(int id)
        {
            if (CheckCookies() == true)
            {
                var roleInDb = _context.Roles.Single(c => c.RoleId == id);
                return View("RoleForm", roleInDb);
            }
            else
            {
                return RedirectToAction("Logout","User");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AspRole role)
        {
            if (CheckCookies() == true)
            {
                if (role == null)
                {
                    return View("RoleForm");
                }
                if (role.RoleId == 0)
                {
                    _context.Roles.Add(role);
                }
                else
                {
                    var roleInDb = _context.Roles.Single(c => c.RoleId == role.RoleId);
                    roleInDb.Name = role.Name;
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                return RedirectToAction("Logout","User");
            }
            
        }
    }
}