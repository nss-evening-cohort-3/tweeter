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

        public TweeterRepository() {}

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
    }
}