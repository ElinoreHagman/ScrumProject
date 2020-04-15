using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        //public ActionResult FormalWall()
        //{
        //    var blogDB = new BlogDbContext();
        //    var viewModel = new PostIndexViewModel
        //    {
        //        Posts = blogDB.Posts.ToList()
        //    };
        //    return View(viewModel);
        //}

        public ActionResult FormalWall()
        {
            var blogDB = new BlogDbContext();
            
            {
                var categories = blogDB.Categories.ToList();
                var viewModel = new PostIndexViewModel { Categories = categories };
                return View(viewModel);
            }

           
        }

    }
}
