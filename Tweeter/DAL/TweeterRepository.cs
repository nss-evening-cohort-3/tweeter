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
        public List<Twit> GetTwits()
        {
            int j = 1;
            return Context.Twits.ToList();
        }
        public void AddTwit(Twit _twit)
        {
            Context.Twits.Add(_twit);
            Context.SaveChanges();
        }

    }
}