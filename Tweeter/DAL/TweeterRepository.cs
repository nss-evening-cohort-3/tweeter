using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Tweeter.Models;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        //property
        public TweeterContext Context { get; set; }

        //
        public TweeterRepository()
        {
            //Context = new TweeterContext();
        }

        //
        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        //
        public Twit AddUserToDatabase(ApplicationUser user)
        {
            Context.TweeterUsers.Add(new Twit { BaseUser = user });
            Twit added_user = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser == user);
            return added_user;
        }

        //
        public List<string> GetUserNames()
        {
            List<string> UserNames = Context.TweeterUsers.Select(m => m.BaseUser.UserName).ToList();
            return UserNames;
        }

        //
        public bool UsernameExists(string v)
        {
            Twit user_exists = Context.TweeterUsers.FirstOrDefault(t => t.BaseUser.UserName == v);
            if (user_exists == null)
            {

                return false;

            }   else
            {
                return true;
            }
        }
    }
}