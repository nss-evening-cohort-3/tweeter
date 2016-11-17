using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweeter.Models;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        public TweeterContext Context { get; set; }
        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterRepository() {}

        public List<string> GetUsernames()
        {
            return Context.TweeterUsers.Select(u => u.BaseUser.UserName).ToList();
        }

        public Twit UsernameExistsOfTwit(string v)
        {
            return Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName.ToLower() == v.ToLower());
        }

        public bool UsernameExists(string v)
        {
            // Works if mocked the UserManager
            /*
            if (Context.Users.Any(u => u.UserName.Contains(v)))
            {
                return true;
            }
            return false;
            */
            
            Twit found_twit = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName.ToLower() == v.ToLower());
            if (found_twit != null)
            {
                return true;
            }

            return false;
            
        }

        public List<Tweet> AddTweet()
        {
            return Context.Tweets.ToList();
        }

        public List<Tweet> RemoveTweet()
        {
            var removedTweet = Context.Tweets.SingleOrDefault(m => m.TweetId == 1);
            return if (removedTweet != null)
            {
                Context.Tweets.Remove(removedTweet);
            }
        }
    }
}