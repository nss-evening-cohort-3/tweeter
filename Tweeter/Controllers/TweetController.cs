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
        private TweeterRepository apiTweeterController = new TweeterRepository();

        // GET api/<controller>
        public List<Tweet> Get()
        {
            return apiTweeterController.GetTweets();
        }

        /*
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST api/<controller>
        public void Post([FromBody]Tweet sentTweet)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                Tweet new_tweet = new Tweet
                {
                    Message = sentTweet.Message,
                    ImageURL = sentTweet.ImageURL,
                    CreatedAt = DateTime.Now,
                    Author = // Need the Twit,
                };
                apiTweeterController.AddTweet(new_tweet);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            apiTweeterController.RemoveTweet(id);
        }
    }
}