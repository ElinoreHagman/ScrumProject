using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ScrumProject.Models
{
    public class PostController : Controller
    {
        [Authorize]
        public ActionResult CreatePost()
        {

            var ctx = new BlogDbContext();
            var myList = ctx.Categories.ToList();
            ViewBag.categories = myList;

            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(Post model, HttpPostedFileBase FilePath, string dropdownMenu)
        {

            var ctx = new BlogDbContext();
            var catId = ctx.Categories.FirstOrDefault(p => p.Name == dropdownMenu);
            var post = new Post
            {

                Title = model.Title,
                Content = model.Content,
                PublishedWall = model.PublishedWall,
                PostDateTime = DateTime.Now,
                Category = catId
            };

            if (FilePath != null)
            {

                try
                {
                    var checkextension = Path.GetExtension(FilePath.FileName).ToLower();
                    if (checkextension.ToLower().Contains(".jpg") || checkextension.ToLower().Contains(".jpeg") || checkextension.Contains(".png") || checkextension.Contains(".pdf"))
                    {
                        // path skapar sökväg för att lägga in bilden i projektmappen Images
                        string path = System.IO.Path.Combine(Server.MapPath("~/Files"), System.IO.Path.GetFileName(FilePath.FileName));
                        // relativePath skapar den relativa sökvägen som läggs in i databasen
                        string relativePath = System.IO.Path.Combine("~/Files/" + FilePath.FileName);

                        post.FilePath = relativePath;
                        ctx.Posts.Add(post);
                        FilePath.SaveAs(path);
                        ctx.SaveChanges();
                        ViewBag.FileStatus = "Photo uploaded successfully.";
                    }

                    else
                    {
                        ViewBag.FileStatus = "Only .JPEG and .PNG allowed";
                    }


                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while photo uploading.";
                }
            }
            else
            {
                ctx.Posts.Add(post);
                ctx.SaveChanges();
            }

            ViewBag.EditStatus = "Successful registration";
            return RedirectToAction("FormalWall", "Wall");

        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPost(int postId)
        {
            var blogDB = new BlogDbContext();
            var showPost = new Post();
            showPost = blogDB.Posts.FirstOrDefault(u => u.PostID == postId);


            return View(showPost);

        }

        [HttpPost]
        public ActionResult EditPost(Post model)
        {

            var blogDb = new BlogDbContext();
            var editedPost = new Post();
            editedPost = blogDb.Posts.FirstOrDefault(u => u.PostID == model.PostID);

            editedPost.Title = model.Title;
            editedPost.Content = model.Content;
            //editedPost.CategoryID = model.CategoryID;
            editedPost.PostDateTime = DateTime.Now;
            blogDb.SaveChanges();

            return RedirectToAction("FormalWall", "Wall");

        }

        public ActionResult DeletePost(int id)
        {

            var blogDb = new BlogDbContext();

            var postObject = blogDb.Posts.FirstOrDefault(x => x.PostID == id);

            blogDb.Posts.Remove(postObject);
            blogDb.SaveChanges();


            return RedirectToAction("FormalWall", "Wall");

        }
    }
}