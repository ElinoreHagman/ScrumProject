using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

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
        public async Task<ActionResult> CreatePost(Post model, HttpPostedFileBase FilePath, string dropdownMenu)
        {
            if (!ModelState.IsValid)
            {
                var db = new BlogDbContext();
                var myList = db.Categories.ToList();
                ViewBag.categories = myList;
                return View();
            }

            var ctx = new BlogDbContext();
            var user = User.Identity.GetUserId();
            var catId = ctx.Categories.FirstOrDefault(p => p.Name == dropdownMenu);
            var author = ctx.Profiles.FirstOrDefault(p => p.ProfileID == user);

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                PublishedWall = model.PublishedWall,
                PostDateTime = DateTime.Now,
                Category = catId,
                AuthorOfPosts = author
            };

            if (FilePath != null)
            {

                try
                {
                    var checkextension = Path.GetExtension(FilePath.FileName).ToLower();
                    if (checkextension.ToLower().Contains(".jpg") || checkextension.ToLower().Contains(".jpeg") || checkextension.Contains(".png") || checkextension.Contains(".pdf") || checkextension.Contains(".xlsx") || checkextension.Contains(".docx"))
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

                        var postCat = post.Category.Name;
                        var viewModel = new EmailNotifyViewModel();
                        var profiles = ctx.SelectedCategories.Where(x => x.Name == postCat).Select(x => x.ProfileID).ToList();
                        var blogDb = new BlogDbContext();
                        foreach (var p in profiles)
                        {

                            var destinationEmail = blogDb.Users.FirstOrDefault(x => x.UserID == p);
                            var mail = destinationEmail.Email;
                            viewModel.ToEmail = mail;
                            var emailSent = await Index(viewModel);
                        }
                    }


                    else
                    {
                        ViewBag.FileStatus = "Only .JPEG, .PFD, .DOCX, .XLSX and .PNG allowed";
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

                var postCat = post.Category.Name;
                var viewModel = new EmailNotifyViewModel();
                var profiles = ctx.SelectedCategories.Where(x => x.Name == postCat).Select(x => x.ProfileID).ToList();
                var blogDb = new BlogDbContext();
                foreach (var p in profiles)
                {

                    var destinationEmail = blogDb.Users.FirstOrDefault(x => x.UserID == p);
                    var mail = destinationEmail.Email;
                    viewModel.ToEmail = mail;
                    var emailSent = await Index(viewModel);
                }
            }

            ViewBag.EditStatus = "Successful registration";

            if (post.PublishedWall.Equals("Formell"))
            {
                return RedirectToAction("FormalWall", "Wall");
            }
            else
            {
                return RedirectToAction("InformalWall", "Wall");

            }

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

        public ActionResult EditInformalPost(int postId)
        {
            var blogDB = new BlogDbContext();
            var showPost = new Post();
            showPost = blogDB.Posts.FirstOrDefault(u => u.PostID == postId);

            return View(showPost);

        }

        [HttpPost]
        public ActionResult EditInformalPost(Post model)
        {

            var blogDb = new BlogDbContext();
            var editedPost = new Post();
            editedPost = blogDb.Posts.FirstOrDefault(u => u.PostID == model.PostID);

            editedPost.Title = model.Title;
            editedPost.Content = model.Content;
            //editedPost.CategoryID = model.CategoryID;
            editedPost.PostDateTime = DateTime.Now;
            blogDb.SaveChanges();

            return RedirectToAction("InformalWall", "Wall");

        }

        public ActionResult DeleteInformalPost(int id)
        {

            var blogDb = new BlogDbContext();

            var postObject = blogDb.Posts.FirstOrDefault(x => x.PostID == id);

            blogDb.Posts.Remove(postObject);
            blogDb.SaveChanges();


            return RedirectToAction("InformalWall", "Wall");

        }

        public ActionResult DownloadFile(string filepath)
        {
            var absolutePath = Server.MapPath(filepath);
            var fileSufix = Path.GetExtension(absolutePath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(absolutePath);
            return File(fileBytes, "application/force-download", "downloadedContent"+fileSufix);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailNotifyViewModel model)
        {
            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    // Initialization  
                    string emailMsg = "Dear " + model.ToEmail + ", <br /><br /> A post has been made in one of the categories you subscribe to!<b style='color: blue'> ORU Blog Notification </b> <br /><br /> Thanks & Regards, <br />ORU Bloggsters";
                    string emailSubject = EmailInfo.EMAIL_SUBJECT_DEFAULT + " Blog Notification";

                    // Sends Email  
                    await this.SendEmailAsync(model.ToEmail, emailMsg, emailSubject);


                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return View();

        }


        public async Task<bool> SendEmailAsync(string email, string msg, string subject = "")
        {
            // Initialization.  
            bool isSend = false;

            try
            {
                // Initialization.  
                var body = msg;
                var message = new MailMessage();

                // Settings.  
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress(EmailInfo.FROM_EMAIL_ACCOUNT);
                message.Subject = !string.IsNullOrEmpty(subject) ? subject : EmailInfo.EMAIL_SUBJECT_DEFAULT;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = EmailInfo.FROM_EMAIL_ACCOUNT,
                        Password = EmailInfo.FROM_EMAIL_PASSWORD
                    };

                    smtp.Credentials = credential;
                    smtp.Host = EmailInfo.SMTP_HOST_GMAIL;
                    smtp.Port = Convert.ToInt32(EmailInfo.SMTP_PORT_GMAIL);
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(message);

                    isSend = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // info.  
            return isSend;
        }

        public ActionResult AddCategory(string newCategory)
        {
            if(newCategory.Length < 3)
            {
                TempData["category_info"] = "Write something first";
                return RedirectToAction("CreatePost", "Post");
            }

            var ctx = new BlogDbContext();
            var category = new Category();
            category.Name = newCategory;
            ctx.Categories.Add(category);
            ctx.SaveChanges();

            TempData["category_info"] = "New category added";

            return RedirectToAction("CreatePost", "Post");
        }
    }
}