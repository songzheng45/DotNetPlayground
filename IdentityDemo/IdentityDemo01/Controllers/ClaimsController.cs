using System.Security.Claims;
using System.Web.Mvc;

namespace Users.Controllers
{
    public class ClaimsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return View("Error", new[] { "找不到Claimｓ" });
            }

            return View(identity.Claims);
        }
    }
}