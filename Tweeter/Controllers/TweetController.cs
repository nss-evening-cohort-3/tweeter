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
        public void Post([FromBody]dynamic newTweet)
        {


            DateTime CreatedAt = newTweet.createdAt.Value;
            string ImageURL = newTweet.image_url.Value;
            string Message = newTweet.message.Value;
            //Twit Author = ????
           // List<Tweet> Replies = ????

            Tweet tweetToSave = new Tweet
            {
                //tweet id??
                Message = Message,
                CreatedAt = CreatedAt,
                ImageURL = ImageURL,
                //author
                //replies

            };
            repo.AddTweet(tweetToSave);

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            repo.RemoveTweet(id);
        }
    }
}