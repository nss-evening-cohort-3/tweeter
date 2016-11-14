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
        

        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterRepository() {}

        public List<ApplicationUser> GetAllAppUsers()
        {
            return Context.Users.ToList();

                
        }

        public bool CheckIfUsernameExists(string username)
        {
            List<ApplicationUser> all_users = Context.Users.ToList();

            foreach (appUsername in all_users)
            {
                if (appUsername == username)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return null;
        }
    }
}