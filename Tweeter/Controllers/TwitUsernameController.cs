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

        TweeterRepository repo = new TweeterRepository();

        // GET api/<controller> 
        public IEnumerable<string> Get()
        {
            return repo.GetUsernames();
        }

        // GET api/<controller>/5
        public Dictionary<string, bool> Get(string candidate)
        {
            /*
             { "exists": true}
            */

            Dictionary<string, bool> answer = new Dictionary<string, bool>();
            answer.Add("exists", repo.UsernameExists(candidate));
            //need to run migration otherwise no response and error isn't reported to client
            return answer;

        }

    }
}