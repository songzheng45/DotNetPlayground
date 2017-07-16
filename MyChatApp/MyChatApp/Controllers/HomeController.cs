using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MyChatApp.Models;

namespace MyChatApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string ip = Utils.GetLocalIP();
            ViewBag.ip = ip;

            return View();
        }

        public ActionResult Chat(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.nickname = nickname;

            string ip = Utils.GetLocalIP();
            ViewBag.ip = ip;

            return View();
        }
    }
}
