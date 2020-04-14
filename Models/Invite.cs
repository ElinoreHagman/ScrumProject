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

        public bool Accepted { get; set; }

        public IList<MeetingDateOptions> MeetingOptions { get; set; }

        public string ProfileID { get; set; }
        public Profile InviteReciever { get; set; }

        public int Meeting { get; set; }
        public Invite MeetingInvite { get; set; }

    }
}