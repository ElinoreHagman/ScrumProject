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
            viewmodel.ProfilesToMeetings = ctx.ProfilesToMeetings.ToList();

            viewmodel.Meetings = ctx.Meetings.Where(x => x.EveryoneAnswered == true).ToList();

            ViewBag.people = ctx.Profiles.ToList();
            var user = User.Identity.GetUserId();
            var invites = ctx.Invites.Where(x => x.ProfileID == user).ToList();
            var InviteID = ctx.Invites.Where(x => x.ProfileID == user).Select(x => x.InviteID).Single();
            var MeetingInviteIndex = ctx.MeetingDateOptionsToInvite.Where(x => x.InviteID == InviteID).Select(x => x.MeetingDateOptionID).Single();
            ViewBag.date = ctx.MeetingOptions.Where(x => x.OptionID == MeetingInviteIndex);

            return View(viewmodel);
        }

        public ActionResult SendInvite(MeetingIndexViewModel model, string[] invitedProfile)
        {

            var ctx = new BlogDbContext();
            DateTime combineDateTime = model.Date.Add(model.Time);
            DateTime combineDateTime2 = model.Date2.Add(model.Time2);

            var meeting = new Meeting
            {
                Name = model.MeetingName,
                ProfileId = User.Identity.GetUserId(),
            };
            ctx.Meetings.Add(meeting);
            ctx.SaveChanges();

            var meetingDateOptions = new MeetingDateOptions
            {
                Date = combineDateTime
            };
            var meetingDateOptions2 = new MeetingDateOptions
            {
                Date = combineDateTime2
            };
            ctx.MeetingOptions.Add(meetingDateOptions);
            ctx.MeetingOptions.Add(meetingDateOptions2);
            ctx.SaveChanges();

            foreach(var person in invitedProfile)
            {
                var invite = new Invite
                {
                    MeetingName = model.MeetingName,
                    Accepted = false,
                    ProfileID = person
                };
                ctx.Invites.Add(invite);
                ctx.SaveChanges();

                var relationInvite = new MeetingDateOptionsToInvite
                {
                    InviteID = invite.InviteID,
                    MeetingDateOptionID = meetingDateOptions.OptionID
                };
                var relationInvite2 = new MeetingDateOptionsToInvite
                {
                    InviteID = invite.InviteID,
                    MeetingDateOptionID = meetingDateOptions2.OptionID
                };
                ctx.MeetingDateOptionsToInvite.Add(relationInvite);
                ctx.MeetingDateOptionsToInvite.Add(relationInvite2);

                ctx.SaveChanges();
            }


            return RedirectToAction("Index", "Calendar");
        }

    }
}