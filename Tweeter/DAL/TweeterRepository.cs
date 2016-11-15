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

        public TweeterRepository(TweeterContext context)
        {
            Context = context;
        }

        public TweeterRepository() { }

        // Get all Usernames
        public List<Twit> GetUsernames()
        {
            return Context.TweeterUsers.ToList();
        }

        // Check if username exists
        public bool UsernameExists(string username)
        {
            return GetUsernames().Any(m => m.Username == username);
        }
    }
}