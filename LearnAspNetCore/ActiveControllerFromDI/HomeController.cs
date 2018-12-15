using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveControllerFromDI
{
    public class HomeController : ControllerBase
    {
        public string Desc { get; set; }
        public HomeController()
        {
            Desc = "Create Instance ";
        }

        public IActionResult Index()
        {
            //HttpContext.RequestServices.GetRequiredService()

            return Content(Desc);
        }
    }
}
