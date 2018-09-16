using SRS.Models;
using SRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Areas.Stuff.Controllers
{
    public class MatrialController : Controller
    {
        private ApplicationDbContext _context;
        public MatrialController()
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
        // GET: Stuff/Matrial
        public ActionResult Index()
        {
            
            if (CheckCookies() == true)
            {
                var path = Server.MapPath("~/Content/Matrial/");
                var dir = new DirectoryInfo(path);
                var files = dir.EnumerateFiles().Select(f => f.Name);
                var viewModel = new AspMatrialVM
                {
                    Fname = files,
                    AspCourse = _context.Courses.ToList(),
                    AspMatrial = new AspMatrial()
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            var path = Path.Combine(Server.MapPath("~/Content/Matrial/"), file.FileName);
            var data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            using (var sw = new FileStream(path, FileMode.Create))
            {
                sw.Write(data, 0, data.Length);
            }
            AspMatrial mat = new AspMatrial();
            mat.FilePath = path;
            mat.Desc = "-----------------------------";
            _context.Matrials.Add(mat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}