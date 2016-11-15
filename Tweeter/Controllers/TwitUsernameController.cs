using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweeter.DAL;

namespace Tweeter.Controllers
{
    public class TwitUsernameController : ApiController
    {
        TweeterRepo repo = new TweeterRepo();

        // GET: api/TwitUsername
        public IEnumerable<string> Get()
        {
            return repo.GetAllUsernames();
        }

        // GET: api/TwitUsername/5
        public bool Get(string username)
        {
            return repo.UsernameExists(username);
        }

        // POST: api/TwitUsername
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TwitUsername/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TwitUsername/5
        public void Delete(int id)
        {
        }
    }
}
