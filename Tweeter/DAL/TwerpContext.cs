using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tweeter.Models;

namespace Tweeter.DAL
{
    public class TwerpContext : ApplicationDbContext
    {
        public virtual DbSet<Twerp> Twerps { get; set; }
        public virtual DbSet<Twit> Twits { get; set; }
    }
}