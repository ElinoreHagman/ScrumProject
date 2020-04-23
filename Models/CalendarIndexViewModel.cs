using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class CalendarIndexViewModel
    {
        public List<Meeting> Meetings { get; set; }

        public List<Profile> Profiles { get; set; }

        public List<ProfilesToMeetings> ProfilesToMeetings { get; set; }
    }
}