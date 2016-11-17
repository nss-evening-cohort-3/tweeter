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

        public List<Tweet> GetTweets()
        {
            return Context.Tweets.ToList();
        }

        public int AddTweet(Tweet sentTweet)
        {
            Context.Tweets.Add(sentTweet);
            Context.SaveChanges();

            return Context.Tweets.ToList().Count;
        }
        public int RemoveTweet(int sentTweetId)
        {
            // This should work but doesn't for some reason
            var selectedTweet = Context.Tweets.Where(t => t.TweetId == sentTweetId).FirstOrDefault();

            if (selectedTweet != null)
            {
                Context.Tweets.Remove(selectedTweet);
                Context.SaveChanges();
            }

            return Context.Tweets.ToList().Count;
        }

    }
}