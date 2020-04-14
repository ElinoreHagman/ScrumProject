using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ScrumProject.Models
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(Post model){

            var ctx = new BlogDbContext();
            var post = new Post
            {
                ProfileID = User.Identity.GetUserId(),
                Title = model.Title,
                Content = model.Content,
                PublishedWall = model.PublishedWall,
                PostDateTime = DateTime.Now,
                CategoryID = 1, //Denna måste vi ändra senare
            };
            ctx.Posts.Add(post);
            ctx.SaveChanges();

            return View(post);
        }
    }
}