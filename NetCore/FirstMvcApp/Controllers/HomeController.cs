using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{

    private readonly IClock _clock;

    public HomeController(IClock clock)
    {
        _clock = clock;
    }
    
    public ActionResult Index()
    {
        ViewBag.NowTime = _clock.GetTime();
        return View();
    }
}