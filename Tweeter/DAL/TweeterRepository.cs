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

        public TweeterRepository() {
            Context = new TweeterContext();
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
            int i = 0;
            return Context.Tweets.ToList();
        }

        public Twit GetTwitUser(string user_id)
        {
            int i = 0;
            // user_id comes from TweetController Line 68
            ApplicationUser found_app_user = Context.Users.FirstOrDefault(u => u.Id == user_id);
            Twit found_user = GetTwitFromUsername(found_app_user.UserName);
            return found_user;
        }

        public Twit GetTwitFromUsername(string user_name)
        {
            return Context.TweeterUsers.FirstOrDefault(twit => twit.BaseUser.UserName == user_name);
        }

        public bool FollowUser(string current_user, string user_to_follow)
        {
            if (user_to_follow == current_user)
            {
                return false;
            }
            else
            {
                // 1. Get the Twits based on the usernames.
                Twit found_current = GetTwitFromUsername(current_user);
                Twit found_user_to_follow = GetTwitFromUsername(user_to_follow);

                if (found_current == null || found_user_to_follow == null)
                {
                    return false;
                }

                // 2. Add the user to follow the current user's follows list
                //found_current.Follows.Add(found_user_to_follow);

                // 2a. Create instance of follow
                Follow new_follow = new Follow { TwitFollower = found_current, TwitFollowed = found_user_to_follow };
                Context.AllFollows.Add(new_follow);

                // 3. Save the changes
                Context.SaveChanges();

                // 4. Return whether successfull
                return true;
            }
        }

        public bool UnfollowUser(string current_user, string user_to_unfollow)
        {
            // 1. Get the Twits based on the usernames.
            //Twit found_current = GetTwitFromUsername(current_user);
            ///Twit found_user_to_follow = GetTwitFromUsername(user_to_unfollow);

            // 2. Find & Remove proper Follow instance
            Follow found_follow = Context.AllFollows.FirstOrDefault(
                f => f.TwitFollowed.BaseUser.UserName == user_to_unfollow && f.TwitFollower.BaseUser.UserName == current_user
            );
            Context.AllFollows.Remove(found_follow);
            // 3. Save the changes
            Context.SaveChanges();

            // 4. Return whether successfull
            return true;
        }
    }
}