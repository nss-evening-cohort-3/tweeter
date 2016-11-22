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
            List<Tweet> list_of_tweets = new List<Tweet>();
            list_of_tweets.Add(
                new Tweet
                {
                    TweetId = 1,
                    Message = "Hello!",
                    Author = new Twit { TwitId = 1, BaseUser = new ApplicationUser { UserName = "JakeFromStateFarm" } },
                    CreatedAt = DateTime.Now
                }
                     );
            list_of_tweets.Add(
                new Tweet {
                    TweetId = 2,
                    Message = "Progressive!",
                    Author = new Twit { TwitId = 1, BaseUser = new ApplicationUser { UserName = "FloFroProgressive"} },
                    CreatedAt = DateTime.Now
                }
            );
            list_of_tweets.Add(
                new Tweet
                {
                    TweetId = 1,
                    Message = "Hello!",
                    Author = new Twit { TwitId = 1, BaseUser = new ApplicationUser { UserName = "JakeFromStateFarm" } },
                    CreatedAt = DateTime.Now
                }
             );
            return list_of_tweets;
            //return Repo.GetTweets();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public Dictionary<string, bool> Post([FromBody]TweetViewModel tweet)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();

            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                string user_id = User.Identity.GetUserId();

                Twit found_user = Repo.GetTwitUser(user_id);

                if (found_user != null)
                {
                    Tweet new_tweet = new Tweet
                    {
                        Message = tweet.Message,
                        ImageURL = tweet.ImageURL,
                        Author = found_user,
                        CreatedAt = DateTime.Now
                    };
                    Repo.AddTweet(new_tweet);
                    answer.Add("successful", true);
                } else
                {
                    answer.Add("successful", false);
                }              
            } else
            {
                answer.Add("successful", false);
            }
            return answer;           
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