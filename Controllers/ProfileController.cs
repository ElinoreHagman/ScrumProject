using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class ProfileController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var ctx = new ProfileDbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.ToList()
            };
            return View(viewModel);
        }

        public ActionResult AddProfile(Profile model)
        {
            var ctx = new ProfileDbContext();
            ctx.Profiles.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}