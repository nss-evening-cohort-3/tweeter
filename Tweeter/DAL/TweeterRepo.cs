using Microsoft.AspNet.Identity.Owin;
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
        public ApplicationUserManager UserManager { get; set;}

        public TweeterRepo()
        {
            UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            Context = new TweeterContext();
        }

        public TweeterRepo(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterRepo(ApplicationUserManager _userManager)
        {
            UserManager = _userManager;
        }

        public TweeterRepo(TweeterContext _context, ApplicationUserManager _userManager)
        {
            Context = _context;
            UserManager = _userManager;
        }

        public Twit AddTwitToDatabase(ApplicationUser user)
        {
            string UserGUI = user.Id;
            Twit added_user = null;
            if (!Context.TweeterUsers.Any(T => T.BaseUserId == user.Id))
            {
                Twit twit_to_add = new Twit { BaseUserId = UserGUI };
                Context.TweeterUsers.Add(twit_to_add);
                Context.SaveChanges();
                added_user = Context.TweeterUsers.FirstOrDefault(u => u.BaseUserId == UserGUI);
                return added_user;
            }
            return added_user;

        }


        public List<string> GetAllUsernames()
        {
            List<string> Usernames = UserManager.Users.Select(u => u.UserName).ToList();
            return Usernames;
        }

        public bool UsernameExists(string username)
        {

            ApplicationUser found_user = UserManager.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
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
            Twit found_twit = Context.TweeterUsers.FirstOrDefault(t => t.BaseUserId == user.Id);
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
            Context.SaveChanges();
        }

        public Tweet GetTweetById(int id)
        {
            return Context.Tweets.FirstOrDefault(t => t.TweetId == id);
        }

        public void DeleteSpecificTweet(Tweet tweet)
        {
            Context.Tweets.Remove(tweet);
            Context.SaveChanges();

        }

        public void DeleteSpecificTweet(int id)
        {
            Tweet found_tweet = Context.Tweets.FirstOrDefault(t => t.TweetId == id);
            if (found_tweet != null)
            {
                Context.Tweets.Remove(found_tweet);
                Context.SaveChanges();

            }
        }

        public List<Tweet> GetAllTweets()
        {
            return Context.Tweets.ToList();
        }

        public List<Tweet> GetAllUserSpecificTweets(int twitId)
        {
            return Context.Tweets.Where(t => t.Author.TwitId == twitId).ToList();
        }

        public void FollowUser(ApplicationUser current_user, int twit_to_follow_id)
        {
            Twit found_current_twit = FindTwitBasedOnApplicationUser(current_user);
            Twit found_twit_to_follow = Context.TweeterUsers.FirstOrDefault(t => t.TwitId == twit_to_follow_id);

            bool does_it_exist_in_users_current_follow_list;
            if (found_current_twit.Follows != null)
            {
                does_it_exist_in_users_current_follow_list = found_current_twit.Follows.Any(t => t == found_twit_to_follow);
            } else
            {
                does_it_exist_in_users_current_follow_list = false;
            }
 

            if (!does_it_exist_in_users_current_follow_list && found_twit_to_follow.TwitId != found_current_twit.TwitId)
            {
                found_current_twit.Follows.Add(found_twit_to_follow);
                Context.SaveChanges();
            }
        }

        public void UnfollowUser(ApplicationUser current_user, int twit_to_follow_id)
        {
            Twit found_current_twit = FindTwitBasedOnApplicationUser(current_user);
            Twit found_twit_to_follow = Context.TweeterUsers.FirstOrDefault(t => t.TwitId == twit_to_follow_id);


            bool does_it_exist_in_users_current_follow_list;
            if (found_current_twit.Follows != null)
            {
                does_it_exist_in_users_current_follow_list = found_current_twit.Follows.Any(t => t == found_twit_to_follow);
            }
            else
            {
                does_it_exist_in_users_current_follow_list = false;
            }

            if (does_it_exist_in_users_current_follow_list && found_twit_to_follow.TwitId != found_current_twit.TwitId)
            {
                found_current_twit.Follows.Remove(found_twit_to_follow);
                Context.SaveChanges();
            }
        }

        public List<Twit> ListTwitsUserIsFollowing(ApplicationUser current_user)
        {
            Twit found_current_twit = FindTwitBasedOnApplicationUser(current_user);
            List<Twit> following = found_current_twit.Follows.ToList();
            return following;
        }

        public bool IsCurrentUserFollowingCurrentlyViewedTwit(ApplicationUser current_user, string username)
        {
            Twit found_current_twit = FindTwitBasedOnApplicationUser(current_user);
            ApplicationUser found_user_to_follow = UserManager.Users.FirstOrDefault(u => u.UserName == username);
            Twit found_twit_to_follow = Context.TweeterUsers.FirstOrDefault(t => t.BaseUserId == found_user_to_follow.Id);


            bool does_it_exist_in_users_current_follow_list;
            if (found_current_twit.Follows != null)
            {
                does_it_exist_in_users_current_follow_list = found_current_twit.Follows.Any(t => t == found_twit_to_follow);
            }
            else
            {
                does_it_exist_in_users_current_follow_list = false;
            }

            return does_it_exist_in_users_current_follow_list;
        }
    }
}