using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Tweeter.DAL;
using Tweeter.Models;
using Microsoft.AspNet.Identity.Owin;

namespace Tweeter.Controllers
{
    public class TweetController : ApiController
    {
        TweeterRepo repo = new TweeterRepo();
        ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: api/Tweet
        public IEnumerable<Tweet> Get()
        {
            return repo.GetAllTweets();
        }

        // GET: api/Tweet/5
        public IEnumerable<Tweet> Get(int id)
        {
            return repo.GetAllUserSpecificTweets(id);
        }

        // POST: api/Tweet
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tweet/5
        public void Post([FromBody]Tweet tweet)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Twit twit_author = repo.FindTwitBasedOnApplicationUser(user);
            tweet.Author = twit_author;
            tweet.CreatedAt = DateTime.Now;
            repo.AddTweet(tweet);
        }

        // DELETE: api/Tweet/5
        public void Delete(int id)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Twit twit_author = repo.FindTwitBasedOnApplicationUser(user);

            Tweet tweet_to_delete = repo.GetTweetById(id);
            if (tweet_to_delete.Author == twit_author)
            {
                repo.DeleteSpecificTweet(id);
            }
            else
            {
                throw new Exception("User is not logged in");
            }
        }
    }
}
