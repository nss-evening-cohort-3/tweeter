using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tweeter.Controllers
{
    public class TweeterController : Controller
    {
        // GET: Tweeter
        public ActionResult Index()
        {
            return View();
        }
    }
}