using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRS.Models; 

namespace SRS.Areas.Stuff.Controllers
{
    public class UserController : Controller
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
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Stuff/Home
        public ActionResult Index()
        {

            if (CheckCookies() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout");
            }

        }

        public ActionResult Stuff()
        {

            if (CheckCookies() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout");
            }

        }

        public ActionResult Student()
        {

            if (CheckCookies() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout");
            }

        }
        public ActionResult Logout()
        {
            if (Request.Cookies["User"] != null)
            {
                HttpCookie myCookie = new HttpCookie("User");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Index","Home",new { area = ""});
        }


        public ActionResult Add(AspUserVm viewModel)
        {

            if (CheckCookies() == true)
            {
                viewModel = new AspUserVm
                {
                    AspUser = new AspUser(),
                    AspRole = _context.Roles.ToList(),
                    AspStudentLvl = _context.StudentLvl.ToList()
                };

                if (_context.Users.Any(c => c.StudentID != "" && c.AspStudentLvl.lvlName == "One"))
                {
                    var _lastStudentId = _context.Users.Last(c => c.AspStudentLvl.lvlName == "One").StudentID;
                    ViewData["lvlOneIDs"] = _lastStudentId;
                }
                else
                {
                    ViewData["lvlOneIDs"] = 4111000;
                }

                // ------------------------------------

                if (_context.Users.Any(c => c.StudentID != "" && c.AspStudentLvl.lvlName == "Two"))
                {
                    var _lastStudentId = _context.Users.Where(c => c.AspStudentLvl.lvlName == "Two").Max(x => x.StudentID);
                    ViewData["lvlTwoIDs"] = _lastStudentId;
                }
                else
                {
                    ViewData["lvlTwoIds"] = 4121000;
                }

                if (_context.Users.Any(c => c.StudentID != "" && c.AspStudentLvl.lvlName == "Three"))
                {
                    var _lastStudentId = _context.Users.Last(c => c.AspStudentLvl.lvlName == "Three").StudentID;
                    ViewData["lvlThreeIDs"] = _lastStudentId;
                }
                else
                {
                    ViewData["lvlThreeIDs"] = 4131000;
                }

                if (_context.Users.Any(c => c.StudentID != "" && c.AspStudentLvl.lvlName == "Four"))
                {
                    var _lastStudentId = _context.Users.Where(c => c.AspStudentLvl.lvlName == "Four").Max(x => x.StudentID);
                    ViewData["lvlFourIds"] = _lastStudentId;
                }
                else
                {
                    ViewData["lvlFourIds"] = 4141000;
                }
                return View("UserForm", viewModel);
            }
            else
            {
                return RedirectToAction("Logout");
            }

        }

        public ActionResult Update(int id)
        {
            
            if (CheckCookies() == true)
            {
                var userInDb = _context.Users.Single(c => c.UserId == id);
                var viewModel = new AspUserVm
                {
                    AspUser = userInDb,
                    AspRole = _context.Roles.ToList(),
                    AspStudentLvl = _context.StudentLvl.ToList()
                };
                return View("UserForm", viewModel);
            }
            else
            {
                return RedirectToAction("Logout","User");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AspUserVm user)
        {

            if (CheckCookies() == true)
            {
                AspUser _user = new AspUser
                {
                    UserId = user.AspUser.UserId,
                    FirstName = user.AspUser.FirstName,
                    LastName = user.AspUser.LastName,
                    Email = user.AspUser.Email,
                    Phone = user.AspUser.Phone,
                    AspRoleId = user.AspUser.AspRoleId,
                    Password = Crypto.encrypt(user.AspUser.Password),
                    AspStudentLvlId = user.AspUser.AspStudentLvlId,
                    StudentID = user.AspUser.StudentID,
                    AuthKey = user.AspUser.AuthKey
                };
                if (!ModelState.IsValid)
                {
                    var viewModel = new AspUserVm
                    {
                        AspUser = _user,
                        AspRole = _context.Roles.ToList(),
                        AspStudentLvl = _context.StudentLvl.ToList()
                    };
                    return View("UserForm", viewModel);
                }

                if (user.AspUser.UserId == 0)
                {
                    _context.Users.Add(_user);
                }
                else
                {
                    var userInDb = _context.Users.Single(c => c.UserId == _user.UserId);
                    _context.Users.Remove(userInDb);
                    _context.SaveChanges();
                    userInDb.FirstName = _user.FirstName;
                    userInDb.LastName = _user.LastName;
                    userInDb.Email = _user.Email;
                    userInDb.Phone = _user.Phone;
                    userInDb.UserId = _user.UserId;
                    userInDb.AspRoleId = _user.AspRoleId;
                    _context.Users.Add(userInDb);

                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Logout","User");
            }
        }
    }
}