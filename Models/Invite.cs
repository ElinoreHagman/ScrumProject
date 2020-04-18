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

        [Required]
        public string MeetingName { get; set; }

        public bool Accepted { get; set; }

        public IList<MeetingDateOptions> MeetingOptions { get; set; }

        [ForeignKey("InviteReciever")]
        public string ProfileID { get; set; }
        public Profile InviteReciever { get; set; }

    }
}