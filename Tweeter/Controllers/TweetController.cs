using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweeter.DAL;
using Tweeter.Models;
using static Tweeter.Models.TweeterViewModels;

namespace Tweeter.Controllers
{
    public class TweetController : ApiController
    {
        TweeterRepository Repo = new TweeterRepository();

        // GET api/<controller>
        public IEnumerable<Tweet> Get()
        {
            return Repo.GetTweets();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]TweetViewModel tweet)
        {
            
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                string user_id = User.Identity.GetUserId();
                ApplicationUser found_app_user = Repo.Context.Users.FirstOrDefault(u => u.Id == user_id);
                Twit found_user = Repo.Context.TweeterUsers.FirstOrDefault(twit => twit.BaseUser.UserName == found_app_user.UserName);
                Tweet new_tweet = new Tweet
                {
                    Message = tweet.Message,
                    ImageURL = tweet.ImageURL,
                    Author = found_user,
                    CreatedAt = DateTime.Now
                };
                Repo.AddTweet(new_tweet);
            }
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            Repo.RemoveTweet(id);
        }
    }
}