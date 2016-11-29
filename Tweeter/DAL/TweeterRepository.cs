using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweeter.Models;
using static Tweeter.DAL.TweeterRepository;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        public TweeterContext Context { get; set; }
        public object Follows { get; private set; }

        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterRepository() {

        }

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
        public Twit GetTwitUser(string AppUserId)//tried passing in Twit, TwitId, Twits
        {
            //this is a way to search thru login credentialed string for the BaseUser Application User that is associ with a Twit. The Twit is the profile and the app user is an auth model that is tied to it.
            Twit found_twit = Context.TweeterUsers.FirstOrDefault(t => t.BaseUser.Id == AppUserId);
            return found_twit;
        }
        //follow a twit 
        public void FollowTwitUser(string AppUserId)
        {
            List<string> Follows = new List<string>();
            Follows.Add(AppUserId);
            Context.SaveChanges();
        }
        public void UnfollowTwitUser(string AppUserId)
        {
            Context.Follows.ToString();
            if (AppUserId != null )
            {
                //remove it
                Follows.Remove(AppUserId) ;
                Context.SaveChanges();
            }
        }
    }
}
