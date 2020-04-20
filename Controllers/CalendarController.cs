using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
            viewmodel.ProfilesToMeetings = ctx.ProfilesToMeetings.ToList();

            ViewBag.people = ctx.Profiles.ToList();

            return View(viewmodel);
        }

        public ActionResult SendInvite(MeetingIndexViewModel model, string dropdownMenu)
        {
            var ctx = new BlogDbContext();
            var meeting = new Meeting
            {
                Name = model.MeetingName,
                ProfileId = User.Identity.GetUserId()
            };
            ctx.Meetings.Add(meeting);
            ctx.SaveChanges();

            var meetingDateOptions = new MeetingDateOptions
            {
                Date = model.MeetingDate
            };
            ctx.MeetingOptions.Add(meetingDateOptions);
            ctx.SaveChanges();

            var invite = new Invite
            {
                MeetingName = model.MeetingName,
                Accepted = false,
                ProfileID = dropdownMenu
            };
            ctx.Invites.Add(invite);
            ctx.SaveChanges();

            var relationInvite = new MeetingDateOptionsToInvite
            {
                InviteID = invite.InviteID,
                MeetingDateOptionID = meetingDateOptions.OptionID
            };
            ctx.MeetingDateOptionsToInvite.Add(relationInvite);
            ctx.SaveChanges();

            return View();
        }
    }
}