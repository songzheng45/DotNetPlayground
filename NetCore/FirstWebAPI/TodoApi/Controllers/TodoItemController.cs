using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi.Environments;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoItemController : Controller
    {
        private readonly Configuration configuration;
        private readonly ILogger<TodoItemController> _logger;

        public TodoItemController(Configuration configuration, ILogger<TodoItemController> logger)
        {
            this.configuration = configuration;
            this._logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", configuration.Database.DBName };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //throw new ArgumentException("hahahaha,Errors!", nameof(id));
            _logger.LogDebug("This is a Debug log");
            _logger.LogError("This is a Error log");
            _logger.LogCritical("This is a Critical log");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
