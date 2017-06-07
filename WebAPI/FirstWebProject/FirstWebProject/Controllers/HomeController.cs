using System;
using System.Web.Http;

namespace FirstWebProject.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }
    }
}
