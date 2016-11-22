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

        public void AddTweet(Tweet a_tweet)
        {
            Context.Tweets.Add(a_tweet);
            Context.SaveChanges();
        }

        public void AddTweet(string username, string tweet_message)
        {
            Twit found_twit = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName == username);
            if (found_twit != null)
            {
                Tweet new_tweet = new Tweet
                {
                    Message = tweet_message,
                    CreatedAt = DateTime.Now,
                    Author = found_twit
                };
                Context.Tweets.Add(new_tweet);
                Context.SaveChanges();
            }
        }

        public Tweet RemoveTweet(int tweet_id)
        {
            Tweet found_tweet = Context.Tweets.FirstOrDefault(t => t.TweetId == tweet_id);
            if (found_tweet != null)
            {
                Context.Tweets.Remove(found_tweet);
                Context.SaveChanges();
            }
            return found_tweet;
        }

        public List<Tweet> GetTweets()
        {
            return Context.Tweets.ToList();
        }
    }
}