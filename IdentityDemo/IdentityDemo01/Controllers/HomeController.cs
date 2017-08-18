using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData(nameof(Index)));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View(nameof(Index), GetData(nameof(OtherAction)));
        }

        public Dictionary<string, object> GetData(string actionName)
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Action", actionName);
            dict.Add("User", HttpContext.User.Identity.Name);
            dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
            dict.Add("In User Role", HttpContext.User.IsInRole("Users"));
            return dict;
        }

        [Authorize]
        public ActionResult UserProp()
        {
            return View(CurrentUser);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UserProp(Cities city)
        {
            var user = CurrentUser;
            user.City = city;
            await UserManager.UpdateAsync(user);
            return View(user);
        }

        public AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        public AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
    }
}