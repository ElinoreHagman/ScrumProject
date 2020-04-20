using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class MeetingIndexViewModel
    {
        public string MeetingName { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Invited { get; set; }
        public IList<Profile> profiles { get; set; }
    }
}