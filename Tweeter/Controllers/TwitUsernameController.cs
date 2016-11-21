using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Tweeter.DAL;
using Tweeter.Models;
using Microsoft.AspNet.Identity.Owin;

namespace Tweeter.Controllers
{
    public class TwitUsernameController : ApiController
    {
        TweeterRepo repo = new TweeterRepo();
        ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();


        // GET: api/TwitUsername

        public IEnumerable<string> Get()
        {
            return repo.GetAllUsernames();
        }

        public string Get(int id)
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            return user.UserName;
        }

        // GET: api/TwitUsername/5
        public Dictionary<string, bool> Get(string usernameCandidate)
        {
            Dictionary<string, bool> answer = new Dictionary<string, bool>();
            answer.Add("exists", repo.UsernameExists(usernameCandidate));
            return answer;
        }

        // POST: api/TwitUsername
        public void Post()
        {
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            repo.AddTwitToDatabase(user);
        }

        // PUT: api/TwitUsername/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TwitUsername/5
        public void Delete(int id)
        {
        }
    }
}
