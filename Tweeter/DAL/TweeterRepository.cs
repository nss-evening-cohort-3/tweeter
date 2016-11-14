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
            List<Twit> TwitList = new List<Twit>(); 
            int j = 1;
            //create an actual list of twits
            //add each Twit to a list called TwitList
            TwitList.Add.Twit();
            return TwitList;
            //tried: 
            //return List<Twit>(Twit).ToString();
            //return List<Twit>.ToString();
            //return List<Twit>().ToString();
            //return List<Twits>().ToString();
            //instantiated TwitList and then returned:
            //TwitList.Add(Twit);
            //TwitList.Add.Twit;
            //TwitList.Add.Twit();
            //
            //
        }
        public void AddTwit(Twit _twit)
        {
            Context.Twit.Add(_twit);
            Context.SaveChanges();
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