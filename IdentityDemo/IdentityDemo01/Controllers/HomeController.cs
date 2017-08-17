using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

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
    }
}
