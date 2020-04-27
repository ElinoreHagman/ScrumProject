using ScrumProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult Chat()
        {
            var dateNow = DateTime.Now.Date;
            var ctx = new BlogDbContext();
            var Profiles = ctx.Profiles.ToList();
            var oldMessages = ctx.Messages.OrderByDescending(x=> x.Date).Where(x=> x.Date > dateNow).Take(5).ToList();
            oldMessages.Reverse();
            ViewBag.OldMessages = oldMessages;

            return View("ChatView");
        }

        public ActionResult NotApprovedPage()
        {
            return View();
        }

    }
}