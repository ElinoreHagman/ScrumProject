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
            var InviteIDs = ctx.Invites.Where(x => x.ProfileID == user && x.Accepted == false).Select(x => x.InviteID).ToList();

            ViewBag.inviteInformation = new List<InviteModel>();

            foreach (var invite in InviteIDs)
            {
                var DateIds = new List<int>();
                DateIds = ctx.MeetingDateOptionsToInvite.Where(x => x.InviteID == invite).Select(x => x.MeetingDateOptionID).ToList();

                var dateDetails = new List<DateTime>();

                foreach (var dateid in DateIds)
                {
                    dateDetails.Add(ctx.MeetingOptions.Where(x => x.OptionID == dateid).Select(x => x.Date).Single());
                }

                string meetingName = ctx.Invites.Where(x => x.InviteID == invite).Select(x => x.MeetingName).Single();

                var inviteTemplate = new InviteModel()
                {
                    MeetingName = meetingName,
                    Creator = ctx.Meetings.Where(x => x.Name == meetingName).Select(x => x.ProfileId).Single(),
                    DateSuggestion1 = dateDetails[0],
                    DateSuggestion2 = dateDetails[1],
                    InviteId = invite
                };

                ViewBag.inviteInformation.Add(inviteTemplate);

            }


            return View(viewmodel);
        }

        public ActionResult AnswerInvite(string IsChecked, string inviteId, string meetingName)
        {
            int inviteNumber = Int32.Parse(inviteId);

            var ctx = new BlogDbContext();

            if (IsChecked != "False")
            {
                DateTime chosenDate = Convert.ToDateTime(IsChecked);

                var invite = ctx.Invites.FirstOrDefault(x => x.InviteID == inviteNumber);
                invite.ChosenDate = chosenDate;
                invite.Accepted = true;
                ctx.SaveChanges();
                TempData["accepted"] = "Du har skickat datumförslag för mötet!";

            } else
            {

                var invite = ctx.Invites.FirstOrDefault(x => x.InviteID == inviteNumber);
                ctx.Invites.Remove(invite);
                var inviteDates = ctx.MeetingDateOptionsToInvite.Where(x => x.InviteID == inviteNumber).ToList();
                ctx.MeetingDateOptionsToInvite.RemoveRange(inviteDates);
                ctx.SaveChanges();
            }

            var allInvitesInMeetings = ctx.Invites.Where(x => x.MeetingName == meetingName).ToList();
            bool EveryoneAnswered = true;

            foreach(var invite in allInvitesInMeetings)
            {
                if(!invite.Accepted)
                {
                    EveryoneAnswered = false;
                }
            }

            if(EveryoneAnswered)
            {
                var meeting = ctx.Meetings.FirstOrDefault(x => x.Name == meetingName);
                meeting.EveryoneAnswered = true;
                ctx.SaveChanges();
            }

            return RedirectToAction("Index", "Calendar");
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