using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityDemo01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            IdentityUser


            return View();
        }
    }
}
