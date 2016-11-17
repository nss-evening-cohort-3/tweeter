using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweeter.DAL;

namespace Tweeter.Controllers
{
    public class TweetController : ApiController
    {
        TweeterRepository repo = new TweeterRepository();

        // GET: api/Tweet
        public IEnumerable<Object> Get()
        {
            return repo.GetTweets();
        }

        // POST: api/Tweet
        public void Post([FromBody]Object value)
        {
            return repo.AddTweet();
        }

        // PUT: api/Tweet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tweet/5
        public void Delete(int id)
        {
        }
    }
}
