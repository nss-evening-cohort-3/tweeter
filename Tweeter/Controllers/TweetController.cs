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
    public class TweetController : ApiController
    {
        TweeterRepository repo = new TweeterRepository();

        // GET api/<controller>
        public IEnumerable<Tweet> Get()
        {
            return repo.GetTweets();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]Tweet tweet)
        {
            repo.AddTweet(tweet);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(Tweet tweet)
        {
            repo.RemoveTweet(tweet);
        }
    }
}