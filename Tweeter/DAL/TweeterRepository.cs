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
            return Context.TweeterUsers.ToList();

        }
        public void AddTwit(Twit _twit)
        {
            Context.TweeterUsers.Add(_twit);
            Context.SaveChanges();
        }
        public List<string> GetUsernames()
        {
            //using LInq here kind of like a foreach;use LInq to iterate over a collection.
            return Context.TweeterUsers.Select(t => t.BaseUser.UserName).ToList();
        }







        public Twit findTwitByTwitId(object Twit)
        {
            Twit foundTwit = new Models.Twit();

            if (foundTwit != null)
            {
                return foundTwit;
            }
            else
            {
                return null;
            }  
        }
    }
}