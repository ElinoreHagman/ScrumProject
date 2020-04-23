﻿using System;
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

        public ActionResult SelectCategory()
        {
            var ctx = new BlogDbContext();
            var myList = ctx.SelectedCategories.ToList();
            ViewBag.SelectedCategories = myList;
            var mySecondList = ctx.Categories.ToList();
            ViewBag.categories = mySecondList;
            var viewModel = new ProfileIndexViewModel
            {
                ChosenCategory = ctx.SelectedCategories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SelectCategory(ChosenCategories model, string dropdownMenu)
        {
            var ctx = new BlogDbContext();
            var mySecondList = ctx.Categories.ToList();
            ViewBag.categories = mySecondList;
            //var chosenCategoryList = ctx.SelectedCategories.ToList();
            var catName = ctx.Categories.FirstOrDefault(p => p.Name == dropdownMenu);
            var userId = User.Identity.GetUserId();
            var chosenCategories = new ChosenCategories
            {
                //ChosenCategoryID = model.ChosenCategoryID,
                Name = catName.Name,
                ProfileID = userId,
            };

            

            ctx.SelectedCategories.Add(chosenCategories);
            ctx.SaveChanges();
            return View();

            //return RedirectToAction("ShowProfile", "Profile");
        }

        [HttpGet]
        public ActionResult SelectProfile()
        {
            var ctx = new BlogDbContext();
            var myList = ctx.Profiles.ToList();
            ViewBag.profiles = myList;


            return View();
        }
    }
}