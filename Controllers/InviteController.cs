using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class InviteController : Controller
    {
        // GET: Invite
        public ActionResult Index()
        {
            var ctx = new BlogDbContext();
            var viewModel = new InviteIndexViewModel
            {
                Invites = ctx.Invites.ToList()
            };
            return View(viewModel);
        }
    }
}