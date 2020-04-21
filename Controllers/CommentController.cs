using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult CreateComment(int postId)
        {

            Session["postId"] = postId;

            return View();
        }


        [HttpPost]
        public ActionResult CreateComment(Comment model)
        {

            var blogDb = new BlogDbContext();
            //var commentedPost = blogDb.Posts.FirstOrDefault(u => u.PostID == post.PostID);
            var comment = new Comment
            {
                Text = model.Text,
                Date = DateTime.Now,
                ProfileID = User.Identity.GetUserId(),
                PostID = Convert.ToInt32(Session["postId"]),
            };
            
            blogDb.Comments.Add(comment);
            blogDb.SaveChanges();


            return View();

        }
    }
}