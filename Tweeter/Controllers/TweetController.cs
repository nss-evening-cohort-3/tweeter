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
            var test = apiTweeterController.GetTweets();

            return test;
        }

        // POST api/<controller>
        public void Post([FromBody]TweetViewModel tweet)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                Tweet new_tweet = new Tweet
                {
                    Message = tweet.Message,
                    ImageURL = tweet.ImageURL,
                    Author = apiTweeterController.FindTwitUser(User.Identity.GetUserId()),
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