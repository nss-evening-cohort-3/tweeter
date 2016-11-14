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

        virtual public List<string> GetUsernames()
        {
            return Context.TweeterUsers.Select(a => a.BaseUser.UserName.ToString()).ToList();
        }

        virtual public bool UsernameExists(string username)
        {
            List<string> usernameList = GetUsernames();

            for (int i = 0; i < usernameList.Count; i++)
            {
                if (username == usernameList[i])
                {
                    return true;
                }
            }
            return false;
        }

    }
}