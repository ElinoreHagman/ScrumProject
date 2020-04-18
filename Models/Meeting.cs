using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Meeting
    {
        [Key]
        public int MeetingID { get; set; }

        [Required(ErrorMessage = "The meeting must have a name")]
        public string Name { get; set; }

        public DateTime MeetingDateTime { get; set; }

        [InverseProperty("AcceptedMeetings")]
        public IList <Profile> ParticipantsWhoAccepted { get; set; }

        [ForeignKey("CreatorOfMeetings")]
        public string ProfileId { get; set; }
        public virtual Profile CreatorOfMeetings { get; set; }

    }

}
