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

        [Authorize]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var ctx = new BlogDbContext();
            var profile = ctx.Profiles.FirstOrDefault(x => x.ProfileID == user);
            var mySecondList = ctx.Categories.ToList();
            ViewBag.categories = mySecondList;
            ViewBag.user = ctx.Users.ToList();

            ViewBag.catNotifications = ctx.SelectedCategories.Where(x=> x.ProfileID == user).ToList();

            var settingsExist = ctx.Settings.FirstOrDefault(x => x.SettingsID == user);

            ViewBag.chosenWall = "None";

            if (settingsExist != null)
            {
                ViewBag.chosenWall = settingsExist.ChosenWall;

            }
            return View(profile);
        }

        public ActionResult UpdateProfile(Profile model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var user = User.Identity.GetUserId();
            var ctx = new BlogDbContext();
            var profile = ctx.Profiles.FirstOrDefault(x => x.ProfileID == user);
            profile.Forename = model.Forename;
            profile.Surname = model.Surname;
            profile.Phonenumber = model.Phonenumber;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AdminPage()
        {
            var ctx = new BlogDbContext();
            var user = User.Identity.GetUserId();
            ViewBag.user = ctx.Users.ToList();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.Where(x => x.ProfileID != user).ToList()
            };

            

            return View(viewModel);
        }

        [Authorize]
        public ActionResult ShowProfile()
        {
            var ctx = new BlogDbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.ToList(),
                ChosenCategory = ctx.SelectedCategories.ToList()
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

            return Index();
        }

        [HttpGet]
        public ActionResult EditPhoneNumber()
        {
            var blogDB = new BlogDbContext();
            var userid = User.Identity.GetUserId();
            var profile = new Profile();

            profile = blogDB.Profiles.FirstOrDefault(u => u.ProfileID == userid);

            return View(profile);
        }

        [HttpPost]
        public ActionResult EditPhoneNumber(Profile model)
        {
            var blogDb = new BlogDbContext();
            var userid = User.Identity.GetUserId();
            var editedNumber = new Profile();

            editedNumber = blogDb.Profiles.FirstOrDefault(u => u.ProfileID == userid);

            editedNumber.Phonenumber = model.Phonenumber;
            blogDb.SaveChanges();

            return RedirectToAction("ShowProfile", "Profile");
        }

        [Authorize]
        public ActionResult SelectCategory()
        {
            var ctx = new BlogDbContext();
            var mySecondList = ctx.Categories.ToList();
            ViewBag.categories = mySecondList;
            return View();
        }

        [HttpPost]
        public ActionResult SelectCategory(string dropdownMenu)
        {
            var ctx = new BlogDbContext();
            var mySecondList = ctx.Categories.ToList();
            ViewBag.categories = mySecondList;
            var catName = ctx.Categories.FirstOrDefault(p => p.Name == dropdownMenu);
            var userId = User.Identity.GetUserId();
            var chosenCategories = new ChosenCategories
            {
                Name = catName.Name,
                ProfileID = userId,
            };

            ctx.SelectedCategories.Add(chosenCategories);
            ctx.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public ActionResult SelectProfile()
        {
            var ctx = new BlogDbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.ToList(),
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult ShowOthersProfile()
        {
            var ctx = new BlogDbContext();
            var user = User.Identity.GetUserId();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.Where(x => x.ProfileID != user).ToList(),
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult ChangeSettings(string id)
        {
            var blogDb = new BlogDbContext();

            var EditRights = new Profile();
            EditRights = blogDb.Profiles.FirstOrDefault(u => u.ProfileID == id);

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

        [Authorize]
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
            return RedirectToAction("AdminPage");
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

        [Authorize]
        public ActionResult WallSettings()
        {

            return View();
        }
    }
}
