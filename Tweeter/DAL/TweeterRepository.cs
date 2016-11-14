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

        public TweeterRepository()
        {
            Context = new TweeterContext();
        }

        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public List<Twit> GetUsernames()
        {
            int i = 1;
            return Context.TweeterUsers.ToList();
        }

        public void AddUser(Twit user)
        {
            Context.TweeterUsers.Add(user);
            Context.SaveChanges();
        }

        public void AddUser(ApplicationUser username)
        {
            Twit user = new Twit { BaseUser = username };
            Context.TweeterUsers.Add(user);
            Context.SaveChanges();
        }

        public Twit FindUserByUsername(ApplicationUser username)
        {
            Twit found_user = Context.TweeterUsers.FirstOrDefault(a => a.BaseUser == username);
            return found_user;
        }
    }
}