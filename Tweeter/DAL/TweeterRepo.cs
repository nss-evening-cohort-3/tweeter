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

        public List<Twit> GetAllUsernames()
        {
            return Context.TweeterUsers.ToList();
        }

        public bool UsernameExists(string username)
        {
            Twit found_user = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName == username);
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