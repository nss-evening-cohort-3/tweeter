using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweeter.DAL;
using Tweeter.Models;

namespace Tweeter.Controllers
{
    public class TweeterController : ApiController
    {
        // GET: api/Tweeter
        TweeterRepository repo = new TweeterRepository();
        public IEnumerable<string> Get()
        {
            return repo.GetUsernames();
        }

        // GET: api/Tweeter/5
        public Dictionary<string, bool> Get(string candidate)
        {
            //{"exists": true} best to return JSON structure
            Dictionary<string, bool> answer = new Dictionary<string, bool>();
            answer.Add("exists", repo.UsernameExists(candidate));
            return answer;

            //return repo.UserNameExists(candidate);
        }
    

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
