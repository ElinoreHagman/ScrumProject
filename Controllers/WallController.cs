using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class WallController : Controller
    {

        [Authorize]
        public ActionResult FormalWall()
        {
            ViewBag.loggedInUser = User.Identity.GetUserId();

            var blogDB = new BlogDbContext();
            var viewModel = new PostIndexViewModel
            {
                Profiles = blogDB.Profiles.ToList(),
                Posts = blogDB.Posts.ToList(),
                Categories = blogDB.Categories.ToList(),
                Comments = blogDB.Comments.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Submit(string dropdownMenu)
        {

            var blogDB = new BlogDbContext();
            var viewModel = new PostIndexViewModel();

            if(dropdownMenu == "0")
            {
                viewModel.Posts = blogDB.Posts.ToList();
                viewModel.Categories = blogDB.Categories.ToList();
                viewModel.Comments = blogDB.Comments.ToList();
            } else
            {
                viewModel.Posts = blogDB.Posts.Where(p => p.Category.Name == dropdownMenu).ToList();
                viewModel.Categories = blogDB.Categories.ToList();
                viewModel.Comments = blogDB.Comments.Where(p => p.PostID == p.Comments.PostID).ToList();
            }

            return View("FormalWall", viewModel);

        }
        [Authorize]
        public ActionResult InformalWall()
        {
            ViewBag.loggedInUser = User.Identity.GetUserId();

            var blogDB = new BlogDbContext();
            var viewModel = new PostIndexViewModel
            {
                Profiles = blogDB.Profiles.ToList(),
                Posts = blogDB.Posts.ToList(),
                Categories = blogDB.Categories.ToList()
            };
            return View(viewModel);
        }

    }
}
