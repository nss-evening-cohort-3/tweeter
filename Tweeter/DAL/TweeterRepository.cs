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

        public TweeterRepository() { }

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
            //return Context.TweeterUsers.Select(u => u.BaseUser.UserName).ToList();
            return Context.Tweets.ToList();
        }

        public void AddTweet(Tweet tweet)
        {
            Context.Tweets.Add(tweet);
            Context.SaveChanges();
        }

        public void DeleteTweet(Tweet tweet)
        {
            Context.Tweets.Remove(tweet);
            Context.SaveChanges();
        }
    }
}