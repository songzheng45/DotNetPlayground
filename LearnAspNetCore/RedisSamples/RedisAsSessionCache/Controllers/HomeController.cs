using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisAsSessionCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly Guid _requestId = Guid.NewGuid();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            string key = "DS_Time";

            //
            // 默认使用同步方式加载 Session 的, 这里调用 await ...LoadAsync() 以异步方式加载
            //
            await HttpContext.Session.LoadAsync();

            string time = HttpContext.Session.GetString(key);
            _logger.LogWarning("RequestId = {0}, time = {1}", _requestId, time);

            if (string.IsNullOrEmpty(time))
            {
                string now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                HttpContext.Session.SetString(key, now);

                //
                // 在设置 Session 的值后, 记得总是以 await ...CommitAsync() 提交更改
                //
                await HttpContext.Session.CommitAsync();

                _logger.LogWarning("RequestId = {0}, set time = {1}", _requestId, now);
            }

            return Content("Done");
        }
    }
}