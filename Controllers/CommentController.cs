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
        [Authorize]
        [HttpGet]
        public ActionResult CreateComment(int postId)
        {

            Session["postId"] = postId;

            return View();
        }


        [HttpPost]
        public ActionResult CreateComment(Comment model)
        {

            var blogDb = new BlogDbContext();
            var user = User.Identity.GetUserId();
            var author = blogDb.Profiles.FirstOrDefault(u => u.ProfileID == user);
            var comment = new Comment
            {
                Text = model.Text,
                Date = DateTime.Now,
                AuthorOfComments = author,
                ProfileID = user,
                PostID = Convert.ToInt32(Session["postId"]),
            };
            
            blogDb.Comments.Add(comment);
            blogDb.SaveChanges();

            var commentedPost = blogDb.Posts.FirstOrDefault(p => p.PostID == comment.PostID);

            if (commentedPost.PublishedWall.Equals("Formell"))
            {

                return RedirectToAction("FormalWall", "Wall");
            }
            else
            {
                return RedirectToAction("InformalWall", "Wall");
            }
        }
    }
}