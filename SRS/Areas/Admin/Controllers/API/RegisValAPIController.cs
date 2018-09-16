using System.Linq;
using System.Web.Http;
using SRS.Models;
using SRS.Models.ViewModel;

namespace SRS.Areas.Admin.Controllers.API
{
    public class RegisValApiController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public RegisValApiController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult CheckGpa(int userId)
        {
            var userGpa = _context.Gpa.Single(c => c.UserId == userId).GpaVal;
            AspCheckGpaVM obj = new AspCheckGpaVM();

            if (userGpa >= 3)
            {
                obj.Gpa = userGpa;
                obj.CoureTaken = 7;
                return Ok(obj);
            }

            if (userGpa == 0 || userGpa >= 2)
            {
                obj.Gpa = userGpa;
                obj.CoureTaken = 6;
                return Ok(obj);
            }

            if ((userGpa < 2 && userGpa > (decimal)1.8) || userGpa == (decimal) 1.8)
            {
                obj.Gpa = userGpa;
                obj.CoureTaken = 5;
                return Ok(obj);
            }

            if ((userGpa < (decimal)1.8 && userGpa > (decimal)1.5) || userGpa == (decimal) 1.5)
            {
                obj.Gpa = userGpa;
                obj.CoureTaken = 4;
                return Ok(obj);
            }

            if (userGpa < (decimal) 1.5)
            {
                obj.Gpa = userGpa;
                obj.CoureTaken = 3;
                return Ok(obj);

            }

            return BadRequest();
        }
    }
}
