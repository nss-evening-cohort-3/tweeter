using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public TweeterRepository() {
            Context = new TweeterContext();
        }

        public List<Twit> GetUsers()
        {
            return Context.TweeterUsers.ToList();
        }

        public Twit AddUser(Twit user)
        {
            Context.TweeterUsers.Add(user);
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var error = e;
            }
            return user;
        }
        public List<string> GetUsernames()
        {
            return Context.TweeterUsers.Select(u => u.BaseUser.UserName).ToList();
        }


        public bool UsernameExists(string v)
        {
            if (Context.Users.Any(u => u.UserName.Contains(v)))
            {
                return true;
            }
            return false;
        }

        public Twit GetUserByName(string name)
        {
            Twit query = Context.TweeterUsers.FirstOrDefault(n => n.TwitName.ToLower() == name);
            return query;
        }
        public List<Tweet> GetTweets()
        {
           return Context.Tweets.ToList();
        }

        public void AddTweet(Tweet new_tweet)
        {
            Context.Tweets.Add(new_tweet);
            Context.SaveChanges();
        }

        public void RemoveTweet(Tweet tweet_to_delete)
        {
            Context.Tweets.Remove(tweet_to_delete);
            Context.SaveChanges();
        }
    }
}