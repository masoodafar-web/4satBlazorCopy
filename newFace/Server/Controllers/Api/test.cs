using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace newFace.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        // GET: api/<test>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<test>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<test>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<test>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<test>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
