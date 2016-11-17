using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweeter.Models;

namespace Tweeter.DAL
{
    public class TweeterRepo
    {
        public TweeterContext Context { get; set; }

        public TweeterRepo()
        {
            Context = new TweeterContext();
        }

        public TweeterRepo(TweeterContext _context)
        {
            Context = _context;
        }

        public Twit AddTwitToDatabase(ApplicationUser user)
        {
            Context.TweeterUsers.Add(new Twit { BaseUser = user });
            Twit added_user = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser == user);
            return added_user;
        }


        public List<string> GetAllUsernames()
        {
            List<string> Usernames = Context.TweeterUsers.Select(m => m.BaseUser.UserName).ToList();
            return Usernames;
        }

        public bool UsernameExists(string username)
        {
            Twit found_user = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName.ToLower() == username.ToLower());
            if (found_user == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public Twit FindTwitBasedOnApplicationUser(ApplicationUser user)
        {
            Twit found_twit = Context.TweeterUsers.FirstOrDefault(t => t.BaseUser == user);
            if (found_twit != null)
            {
                return found_twit;
            } else
            {
                return null;
            }
        }

        public void AddTweet(Tweet tweet)
        {
            Context.Tweets.Add(tweet);
        }

        public Tweet GetTweetById(int id)
        {
            return Context.Tweets.FirstOrDefault(t => t.TweetId == id);
        }

        public void DeleteSpecificTweet(Tweet tweet)
        {
            Context.Tweets.Remove(tweet);
        }

        public void DeleteSpecificTweet(int id)
        {
            Tweet found_tweet = Context.Tweets.FirstOrDefault(t => t.TweetId == id);
            if (found_tweet != null)
            {
                Context.Tweets.Remove(found_tweet);
            }
        }

        public List<Tweet> GetAllTweets()
        {
            return Context.Tweets.ToList();
        }
    }
}