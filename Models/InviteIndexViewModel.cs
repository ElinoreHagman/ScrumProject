using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class InviteIndexViewModel
    {
        public List<Invite> Invites { get; set; }
        public bool Accepted { get; set; }
        public string MeetingName { get; set; }
        public DateTime ChoosenDate { get; set; }
        public DateTime DateSuggested { get; set; }
        public DateTime DateSuggested2 { get; set; }
        public string MeetingCreator { get; set; }
            
    }
}