using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class InviteModel
    {

        public int MeetingID { get; set; }
        public string MeetingName { get; set; }
        public string Creator { get; set; }
        public int InviteId { get; set; }
        public DateTime DateSuggestion1 { get; set; }
        public DateTime DateSuggestion2 { get; set; }

    }
}