using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public List<string> GetUserNames()
        {
            return Context.Users.Select(u => u.UserName).ToList();
        }
        public bool CheckForUserName(string username)
        {
            if (Context.Users.Any(u => u.UserName.Contains(username)))
            {
                return true;
            }
            return false;
        }
    }
}