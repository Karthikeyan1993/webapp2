using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace webapp2.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value100", "value200", "value300" };
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult("ID must be larger than 0");
            }
            return new ObjectResult("test value");
        }
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}