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
    }
}