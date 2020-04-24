using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class MeetingTemplate
    {
        public string MeetingName { get; set; }
        public List<Profile> Participants { get; set; } 
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public int Date1Voters { get; set; }
        public int Date2Voters { get; set; }
        public int MeetingID { get; set; }
    }
}