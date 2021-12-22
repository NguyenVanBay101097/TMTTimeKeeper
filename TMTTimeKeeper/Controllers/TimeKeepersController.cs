using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TMTTimeKeeper.Helpers;
using TMTTimeKeeper.Interface;
using TMTTimeKeeper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TMTTimeKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeKeepersController : ControllerBase
    {
        private readonly ICzkemHelper _czkemHelper = new CzkemHelper();
        public TimeKeepersController(ICzkemHelper czkemHelper)
        {
            _czkemHelper = czkemHelper;
        }
        // GET: api/<TimeKeepersController>
        [HttpGet]
        public IActionResult Get()
        {
           var res = new string[] { "value1", "value2" };
            return Ok(res);
        }

        // GET api/<TimeKeepersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TimeKeepersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TimeKeepersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TimeKeepersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        [HttpPost("[action]")]
        public IActionResult Connect([FromBody] TimeKeeperConnectReq val)
        {
            var res = _czkemHelper.Connect(val.IPAddress,val.TCPPort);
            return Ok(res);
        }
    }
}
