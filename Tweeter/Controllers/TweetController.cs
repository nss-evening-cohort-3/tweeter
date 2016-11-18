﻿using Microsoft.AspNet.Identity;
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
        private TweeterRepository apiTweeterController = new TweeterRepository();

        // GET api/<controller>
        public IEnumerable<Tweet> Get()
        {
            return apiTweeterController.GetTweets();
        }

        // POST api/<controller>
        public void Post([FromBody]TweetViewModel tweet)
        {
            
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                string user_id = User.Identity.GetUserId();
                ApplicationUser found_app_user = apiTweeterController.Context.Users.FirstOrDefault(u => u.Id == user_id);
                Twit found_user = apiTweeterController.Context.TweeterUsers.FirstOrDefault(twit => twit.BaseUser.UserName == found_app_user.UserName);
                Tweet new_tweet = new Tweet
                {
                    Message = tweet.Message,
                    ImageURL = tweet.ImageURL,
                    Author = found_user,
                    CreatedAt = DateTime.Now
                };
                apiTweeterController.AddTweet(new_tweet);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            apiTweeterController.RemoveTweet(id);
        }
    }
}