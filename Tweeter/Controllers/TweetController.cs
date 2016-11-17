using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweeter.DAL;
using Tweeter.Models;

namespace Tweeter.Controllers
{
    public class TweetController : ApiController
    {
        private TweeterRepository repo = new TweeterRepository();
      
        // GET: api/Tweet
        [HttpGet]
        public IEnumerable<Tweet> Get()
        {
           return repo.GetTweets();
        }

        // GET: api/Tweet/5
        public List<Tweet> Get(int id)
        {
            return repo.GetTweets(id);
        }

        // POST: api/Tweet
        [HttpPost]
        [Authorize]
        public void Post([FromBody]Tweet message)
        {
            repo.AddTweet(message);
        }

        // PUT: api/Tweet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tweet/5
        [HttpDelete]
        [Authorize]
        public void Delete(int id)
        {
            repo.RemoveTweet(id);
        }
    }
}
