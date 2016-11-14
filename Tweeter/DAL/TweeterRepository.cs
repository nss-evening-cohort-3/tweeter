using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweeter.Models;
using Microsoft.AspNet.Identity;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        public TweeterContext Context { get; set; }

        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterRepository() {}

        public List<string> GetUsernames()
        {
            //string currentUserId = User.Identity.GetUserName();
            //return currentUserId;
            var UCont = Context.TweeterUsers.Select(a => a.BaseUser.UserName.ToString()).ToList();
            return UCont;
        }

        public bool UsernameExists(string username)
        {
            List<string> usernameList = GetUsernames();
            for (var x = 0; x < usernameList.Count; x++)
            {
                if (usernameList[x] == username)
                {
                    return true;
                }
            }
            return false;
        }
    }
}