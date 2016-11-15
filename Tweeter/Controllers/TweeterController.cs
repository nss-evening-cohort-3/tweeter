using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tweeter.Controllers
{
    public class TweeterController : ApiController
    {
        // GET: api/Tweeter
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tweeter/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tweeter
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tweeter/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tweeter/5
        public void Delete(int id)
        {
        }
    }
}
