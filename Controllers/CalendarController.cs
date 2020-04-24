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
        [Authorize]
        public ActionResult Index()
        {
            var ctx = new BlogDbContext();
            var viewmodel = new CalendarIndexViewModel();
            viewmodel.ProfilesToMeetings = ctx.ProfilesToMeetings.ToList();
            viewmodel.Meetings = ctx.Meetings.Where(x => x.EveryoneAnswered == true && x.MeetingDateTime != null).ToList();
            var user = User.Identity.GetUserId();

            ViewBag.people = ctx.Profiles.Where(x => x.ProfileID != user).ToList();

            var InviteIDs = ctx.Invites.Where(x => x.ProfileID == user && x.Accepted == false).ToList();

            ViewBag.inviteInformation = new List<InviteModel>();

            foreach (var invite in InviteIDs)
            {
                var DateIds = new List<int>();
                DateIds = ctx.MeetingDateOptionsToInvite.Where(x => x.InviteID == invite.InviteID).Select(x => x.MeetingDateOptionID).ToList();

                var dateDetails = new List<DateTime>();

                foreach (var dateid in DateIds)
                {
                    dateDetails.Add(ctx.MeetingOptions.Where(x => x.OptionID == dateid).Select(x => x.Date).Single());
                }

                string meetingName = ctx.Invites.Where(x => x.InviteID == invite.InviteID).Select(x => x.MeetingName).Single();
                var meetingId = ctx.Invites.Where(x => x.InviteID == invite.InviteID).Single();

                var meetingCreator = ctx.Meetings.FirstOrDefault(x => x.MeetingID == meetingId.MeetingID);

                var creatorFirstname = ctx.Profiles.Where(x => x.ProfileID == meetingCreator.ProfileId).Select(x => x.Forename).Single();
                var creatorSurname = ctx.Profiles.Where(x => x.ProfileID == meetingCreator.ProfileId).Select(x => x.Surname).Single();

                var creatorName = creatorFirstname + " " + creatorSurname;

                var inviteTemplate = new InviteModel()
                {
                    MeetingID = invite.MeetingID,
                    MeetingName = invite.MeetingName,
                    Creator = creatorName,
                    DateSuggestion1 = dateDetails[0],
                    DateSuggestion2 = dateDetails[1],
                    InviteId = invite.InviteID
                };

                ViewBag.inviteInformation.Add(inviteTemplate);

            }

            var meetingInformation = ctx.Meetings.Where(x => x.ProfileId == user && x.EveryoneAnswered == true && x.MeetingDateTime == null).ToList();

            ViewBag.meetingInfo = new List<MeetingTemplate>();

            foreach (var meeting in meetingInformation)
            {
                var invited = ctx.ProfilesToMeetings.Where(x => x.MeetingID == meeting.MeetingID).Select(x => x.Profile).ToList();
                var meetingID = meeting.MeetingID;

                var dates = (from dateNames in ctx.MeetingOptions
                             join dateIDs in ctx.MeetingDateOptionsToInvite
                             on dateNames.OptionID equals dateIDs.MeetingDateOptionID
                             join invites in ctx.Invites
                             on dateIDs.InviteID equals invites.InviteID
                             where invites.MeetingID == meetingID
                             select dateNames.Date).ToList();

                if (dates.Count > 0)
                {

                    var date1 = dates[0];
                    var date2 = dates[1];

                    var date1Amount = ctx.Invites.Where(x => x.MeetingID == meeting.MeetingID && x.ChosenDate == date1).ToList();
                    var date2Amount = ctx.Invites.Where(x => x.MeetingID == meeting.MeetingID && x.ChosenDate == date2).ToList();

                    var meetingTemplate = new MeetingTemplate()
                    {
                        MeetingName = meeting.Name,
                        Participants = invited,
                        MeetingID = meeting.MeetingID,
                        Date1 = dates[0],
                        Date2 = dates[1],
                        Date1Voters = date1Amount.Count,
                        Date2Voters = date2Amount.Count,

                    };

                    ViewBag.meetingInfo.Add(meetingTemplate);

                } else
                {
                    var meetingTemplate = new MeetingTemplate()
                    {
                        MeetingName = meeting.Name,

                    };
                    ViewBag.meetingInfo.Add(meetingTemplate);
                }
            }

            return View(viewmodel);
        }

        public ActionResult AnswerInvite(string IsChecked, string inviteId, int meetingID)
        {
            if (IsChecked == null)
            {
                return RedirectToAction("Index", "Calendar");
            }

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

                var user = User.Identity.GetUserId();
                var profile = ctx.Profiles.FirstOrDefault(x => x.ProfileID == user);
                var insertRelation = new ProfilesToMeetings();
                insertRelation.MeetingID = invite.MeetingID;
                insertRelation.Profile = profile;
                ctx.ProfilesToMeetings.Add(insertRelation);
                ctx.SaveChanges();

            } else
            {

                var invite = ctx.Invites.FirstOrDefault(x => x.InviteID == inviteNumber);
                ctx.Invites.Remove(invite);
                var inviteDates = ctx.MeetingDateOptionsToInvite.Where(x => x.InviteID == inviteNumber).ToList();
                ctx.MeetingDateOptionsToInvite.RemoveRange(inviteDates);
                ctx.SaveChanges();
            }

            var allInvitesInMeetings = ctx.Invites.Where(x => x.MeetingID == meetingID).ToList();
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
                var meeting = ctx.Meetings.FirstOrDefault(x => x.MeetingID == meetingID);
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
                    ProfileID = person,
                    MeetingID = meeting.MeetingID
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

        public ActionResult DecideMeeting (DateTime IsChecked, int meetingId)
        {

            var ctx = new BlogDbContext();
            var meeting = ctx.Meetings.FirstOrDefault(x => x.MeetingID == meetingId);
            meeting.MeetingDateTime = IsChecked;
            ctx.SaveChanges();

            return RedirectToAction("Index", "Calendar");
        }

        public ActionResult DeleteOldMeeting(string meetingName)
        {

            var ctx = new BlogDbContext();
            var meeting = ctx.Meetings.FirstOrDefault(x => x.Name == meetingName);
            ctx.Meetings.Remove(meeting);
            ctx.SaveChanges();

            return RedirectToAction("Index", "Calendar");
        }

    }
}