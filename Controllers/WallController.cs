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
            var user = User.Identity.GetUserId();
            ViewBag.loggedInUser = user;
            var blogDB = new BlogDbContext();
            var loggedIn = blogDB.Profiles.FirstOrDefault(x => x.ProfileID == user);
            ViewBag.isAdmin = loggedIn.AdminRights;

            var blogPosts = blogDB.Posts.ToList();
            blogPosts.Reverse();
            var viewModel = new PostIndexViewModel
            {
                Profiles = blogDB.Profiles.ToList(),
                Posts = blogPosts,
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
                viewModel.Profiles = blogDB.Profiles.ToList();
                viewModel.Posts = blogDB.Posts.ToList();
                viewModel.Categories = blogDB.Categories.ToList();
                viewModel.Comments = blogDB.Comments.ToList();
            } else
            {
                viewModel.Profiles = blogDB.Profiles.ToList();
                viewModel.Posts = blogDB.Posts.Where(p => p.Category.Name == dropdownMenu).ToList();
                viewModel.Categories = blogDB.Categories.ToList();
                viewModel.Comments = blogDB.Comments.Where(p => p.PostID == p.Comments.PostID).ToList();
            }

            return View("FormalWall", viewModel);

        }
        [Authorize]
        public ActionResult InformalWall()
        {
            var user = User.Identity.GetUserId();
            ViewBag.loggedInUser = user;
            var blogDB = new BlogDbContext();
            var loggedIn = blogDB.Profiles.FirstOrDefault(x => x.ProfileID == user);
            ViewBag.isAdmin = loggedIn.AdminRights;

            var blogPosts = blogDB.Posts.ToList();
            blogPosts.Reverse();

            var viewModel = new PostIndexViewModel
            {
                Profiles = blogDB.Profiles.ToList(),
                Posts = blogPosts,
                Categories = blogDB.Categories.ToList(),
                Comments = blogDB.Comments.ToList()
            };
            return View(viewModel);
        }

    }
}
