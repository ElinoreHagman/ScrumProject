using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class ProfileController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var ctx = new BlogDbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.ToList()
            };
            return View(viewModel);
        }

        public ActionResult AddProfile(Profile model)
        {
            var ctx = new BlogDbContext();
            ctx.Profiles.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AdminSettings()
        {
            var ctx = new BlogDbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.ToList()
            };
            ////ctx.Profiles.Add(model);
            //ctx.SaveChanges();

            return Index();
        }

        public ActionResult ChangeSettings(string id)
        {
            var blogDb = new BlogDbContext();
            var EditRights = new Profile();
            EditRights = blogDb.Profiles.FirstOrDefault(u => u.ProfileID == id);
            //var currentUser = User.Identity.GetUserId();
            if (EditRights.AdminRights == false)
            {
                EditRights.AdminRights = true;
                blogDb.SaveChanges();
            }
            else
            {
                EditRights.AdminRights = false;
                blogDb.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult AdminAccept()
        {
            var ctx = new BlogDbContext();
            ViewBag.user = ctx.Users.ToList();

            return RedirectToAction("Index");
        }

        public ActionResult AcceptAccount(string id)
        {
            var blogDb = new BlogDbContext();
            var acceptUser = new User();
            acceptUser = blogDb.Users.FirstOrDefault(u => u.UserID == id);
            //var currentUser = User.Identity.GetUserId();
            if (acceptUser.Approved == false)
            {
                acceptUser.Approved = true;
                blogDb.SaveChanges();
            }
            else
            {
                acceptUser.Approved = false;
                blogDb.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DefaultWallSettings(string IsChecked)
        {

            var ctx = new BlogDbContext();
            var user = User.Identity.GetUserId();
            var settingsExist = ctx.Settings.FirstOrDefault(x => x.SettingsID == user);

            if(settingsExist == null)
            {
                var settings = new Settings();
                settings.SettingsID = user;
                settings.ChosenWall = IsChecked;
                ctx.Settings.Add(settings);
            } else
            {
                settingsExist.ChosenWall = IsChecked;
            }
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult WallSettings()
        {

            var ctx = new BlogDbContext();
            var user = User.Identity.GetUserId();
            var settingsExist = ctx.Settings.FirstOrDefault(x => x.SettingsID == user);

            ViewBag.chosenWall = "None";

            if (settingsExist != null)
            {
                ViewBag.chosenWall = settingsExist.ChosenWall;
            }

            return View();
        }
    }
}
