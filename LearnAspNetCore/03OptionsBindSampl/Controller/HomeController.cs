using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

public class HomeController : Controller
{
    /*
     * 依赖注入配置类
     */

    // 方式一：构造器注入，从 Options 中获取配置文件
    // 方式二：在视图中直接使用注入 Inject ，则无需使用方式一

    public readonly Settings _config;
    public readonly DatabaseConfiguration _dbConfig;
    public HomeController(IOptionsSnapshot<Settings> option, IOptionsSnapshot<DatabaseConfiguration> dbConfig)
    {
        // 从 Options 中获取配置文件
        //_configuration = config.Value;

        _config = option.Value;
        _dbConfig = dbConfig.Get("db");
    }

    public IActionResult Index()
    {
        // return View(_config);

        return Content(_config.Database.ConnectionString);
        // //return View();
    }

    public IActionResult DbConfig()
    {
        return Content(_dbConfig.ConnectionString);
    }

}