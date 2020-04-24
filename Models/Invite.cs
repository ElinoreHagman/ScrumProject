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
        public Invite()
        {
            Accepted = false;
        }

        [Key]
        public int InviteID { get; set; }

        [Required]
        public string MeetingName { get; set; }

        public DateTime? ChosenDate { get; set; }

        public bool Accepted { get; set; }

        public IList<MeetingDateOptionsToInvite> MeetingOptions { get; set; }

        [ForeignKey("InviteReciever")]
        public string ProfileID { get; set; }
        public Profile InviteReciever { get; set; }

        [ForeignKey("AssignedToMeeting")]
        public int MeetingID { get; set; }
        public Meeting AssignedToMeeting { get; set; }

    }
}