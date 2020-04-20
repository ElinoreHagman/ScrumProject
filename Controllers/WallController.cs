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
        // GET: Wall
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormalWall()
        {
            var blogDB = new BlogDbContext();
            var viewModel = new PostIndexViewModel
            {
                Posts = blogDB.Posts.ToList(),
                Categories = blogDB.Categories.ToList()
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
            } else
            {
                viewModel.Posts = blogDB.Posts.Where(p => p.Category.Name == dropdownMenu).ToList();
                viewModel.Categories = blogDB.Categories.ToList();
            }

            return View("FormalWall", viewModel);

        }
        public ActionResult InformalWall()
        {
            var blogDB = new BlogDbContext();
            var viewModel = new PostIndexViewModel
            {
                Posts = blogDB.Posts.ToList(),
                Categories = blogDB.Categories.ToList()
            };
            return View(viewModel);
        }
    }
}