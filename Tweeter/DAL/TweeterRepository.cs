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

        public Tweet AddTweet(Tweet addedTweet)
        {
            return Context.Tweets.Add(addedTweet);
            
        }

        public Tweet RemoveTweet(int TweetId)
        {
            var removedTweet = Context.Tweets.SingleOrDefault(m => m.TweetId == TweetId);
            if (removedTweet != null)
            {
                Context.Tweets.Remove(removedTweet);
                Context.SaveChanges();
                return removedTweet;
            }
            return null;           
        }

        public List<Tweet> GetTweets()
        {
            return Context.Tweets.ToList();
        }
    }
}