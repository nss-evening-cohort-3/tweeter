using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweeter.DAL;
using Tweeter.Models;

namespace Tweeter.Controllers
{
    public class FollowController : ApiController
    {
        private TweeterRepository repo = new TweeterRepository();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string user_to_follow)
        {
            if (User.Identity.IsAuthenticated)
            {
                string user_id = User.Identity.GetUserId();
                Twit found_user = repo.GetTwitUser(user_id);
                repo.FollowUser(found_user.BaseUser.UserName,user_to_follow);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(string user_to_unfollow)
        {
            if (User.Identity.IsAuthenticated)
            {
                string user_id = User.Identity.GetUserId();
                Twit found_user = repo.GetTwitUser(user_id);
                repo.UnfollowUser(found_user.BaseUser.UserName, user_to_unfollow);
            }
        }
    }
}