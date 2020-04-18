using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class MeetingDateOptionsToInvite
    {
        [Key, Column(Order = 0)]
        public int InviteID { get; set; }
        [Key, Column(Order = 1)]
        public int MeetingDateOptionID { get; set; }
        public virtual Invite Invite { get; set; }
        public virtual MeetingDateOptions MeetingDateOptions { get; set; }
    }
}