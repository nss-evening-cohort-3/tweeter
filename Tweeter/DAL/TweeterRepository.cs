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

        public List<Twit> GetUsers()
        {
            return Context.TweeterUsers.ToList();
        }

        public Twit AddUser(Twit user)
        {
            Context.TweeterUsers.Add(user);
            Context.SaveChanges();
            return user;
        }

        public Twit GetUserByName(string name)
        {
            Twit query = Context.TweeterUsers.FirstOrDefault(n => n.TwitName.ToLower() == name);
            return query;
        }
    }
}