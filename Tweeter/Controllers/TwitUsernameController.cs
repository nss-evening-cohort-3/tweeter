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
        Random random = new Random();
        TweeterRepository repo = new TweeterRepository();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return repo.GetUsernames();
        }

        // GET api/<controller>/5
        public Dictionary<string,bool> Get(string candidate)
        {
            /*
             { "exists": true}
            */
            int item = random.Next(0, 2);
            bool[] options = new [] { true, false }; 
            Dictionary<string, bool> answer = new Dictionary<string, bool>();
            //answer.Add("exists", repo.UsernameExists(candidate));
            answer.Add("exists", options[item]);
            return answer;

        }

    }
}