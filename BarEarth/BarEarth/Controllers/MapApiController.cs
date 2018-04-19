using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarEarth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarEarth.Controllers
{
    [Produces("application/json")]
    [Route("api/MapApi")]
    public class MapApiController : Controller
    {
        private IEnumerable<Bar> BarsNearby = new List<Bar>();

        //public void ReceiveBarData(? data)

        // GET: api/MapApi
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MapApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/MapApi
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/MapApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
