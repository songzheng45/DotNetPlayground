using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;

public class HomeController : Controller
{

    private readonly IClock _clock;

    public HomeController(IClock clock)
    {
        _clock = clock;
    }
    
    public ActionResult Index()
    {
        //int n = 0;
        //int b = 10 / n;


        ViewBag.NowTime = _clock.GetTime();
        return View();
    }

    public ActionResult About()
    {
        return View();
    }
}