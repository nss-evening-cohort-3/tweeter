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
        public TweeterContext Context { get; set; }

        public TweeterRepository()
        {
            //Context = new TweeterContext();
        }

        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        









    }
}