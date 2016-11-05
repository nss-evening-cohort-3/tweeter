using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAuth.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}