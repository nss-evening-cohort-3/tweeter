using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tweeter.DAL;

namespace Tweeter.Controllers
{
    public class TwitController : Controller
    {

        // GET: Twit
        public ActionResult Index()
        {
            return View();
        }
    }
}