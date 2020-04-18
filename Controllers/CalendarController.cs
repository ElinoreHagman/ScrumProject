﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumProject.Models;

namespace ScrumProject.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            var ctx = new BlogDbContext();
            var viewmodel = new CalendarIndexViewModel();
            viewmodel.Meetings = ctx.Meetings.ToList();

            

            return View(viewmodel);
        }
    }
}