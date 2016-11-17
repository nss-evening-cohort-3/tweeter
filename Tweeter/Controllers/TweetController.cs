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
        // GET: api/Tweet
        public List<Tweet> Get()
        {
            TweeterRepository repo = new TweeterRepository();
            List<Tweet> all_tweets = repo.GetAllTweets();
            return all_tweets;
        }

      

        // POST: api/Tweet
        public void Post([FromBody]Tweet tweet)
        {
            TweeterRepository repo = new TweeterRepository();
            repo.Add(tweet);
            
        }

       

        // DELETE: api/Tweet/5
        public void Delete(int TweetId)
        {
            TweeterRepository repo = new TweeterRepository();
            repo.DeleteTweet(TweetId);
        }


    }
}
