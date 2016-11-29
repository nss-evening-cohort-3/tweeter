using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Tweeter.DAL;
using Microsoft.AspNet.Identity.Owin;
using Tweeter.Models;
using Microsoft.AspNet.Identity;

namespace Tweeter.Controllers
{
    public class TweeterController : ApiController
    {

        TweeterRepo repo = new TweeterRepo();
        ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        // GET: api/Tweeter
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tweeter/5
        public string Get(int id)
        {
            return "value";
        }

        public bool Get(string username)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            bool following = repo.IsCurrentUserFollowingCurrentlyViewedTwit(user, username);
            return following;
        }

        // POST: api/Tweeter
        public void Post([FromBody]dynamic value)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());

            if (value["alreadyFollowing"] == "true")
            {
                value["alreadyFollowing"] = true;
            } else
            {
                value["alreadyFollowing"] = false;
            }


            if (value["alreadyFollowing"])
            {
                repo.UnfollowUser(user, value["userToFollow"]);
            }
            else
            {
                repo.FollowUser(user, value["userToFollow"]);
            }
        }

        // PUT: api/Tweeter/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tweeter/5
        public void Delete(int id)
        {
        }
    }
}
