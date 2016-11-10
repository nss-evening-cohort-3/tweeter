using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tweeter.Models;

namespace Tweeter.DAL
{
    public class TweeterContext : ApplicationDbContext
    {
        public virtual DbSet<Tweet> Tweets { get; set; }
        public virtual DbSet<Twit> TweeterUsers { get; set; }
    }
}