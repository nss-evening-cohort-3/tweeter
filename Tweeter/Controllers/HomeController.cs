using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Tweeter.Models;

namespace Tweeter.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        bool isAuthed;
        

        public ActionResult Index()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            if(ModelState.IsValid)
            {
                ViewBag.IsAuthed = true;
            }
            else
            {
                ViewBag.IsAuthed = false;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}