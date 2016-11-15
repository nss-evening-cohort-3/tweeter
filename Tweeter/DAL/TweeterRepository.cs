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

        public TweeterRepository() {

            Context = new TweeterContext();
        }

        public List<string> GetAllAppUsers()
        {
            return Context.Users.Select(x => x.UserName).ToList();                
        }

        public bool CheckIfUsernameExists(string username)
        {
            List<string> all_users = GetAllAppUsers();

            return all_users.Any(user => username == user);

        }    
    }
}