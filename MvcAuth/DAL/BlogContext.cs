using MvcAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcAuth.DAL
{
    public class BlogContext : ApplicationDbContext
    {
        public virtual DbSet<BlogPost> Posts { get; set; }
    }
}