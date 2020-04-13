using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Invite
    {
        [Key]
        public int InviteID { get; set; }

        [ForeignKey("Getter")]
        public string GetterID { get; set; }
        public Profile Getter { get; set; }

        [ForeignKey("Asker")]
        public string AskerID { get; set; }
        public Profile Asker { get; set; }

        [ForeignKey("RegardsMeeting")]
        public string MeetingID { get; set; }
        public Meeting Meeting { get; set; }
     
    }
}