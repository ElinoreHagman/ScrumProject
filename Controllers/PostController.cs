using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            var ctx =  new BlogDbContext();
            var viewModel = new PostIndexViewModel
            {
                Posts = ctx.Posts.ToList()

            };
            return View(viewModel);
        }
    }
}