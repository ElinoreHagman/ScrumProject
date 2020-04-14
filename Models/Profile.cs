using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public string ProfileID { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Phonenumber { get; set; }
        public bool AdminRights { get; set; }

        public IList<Post> WrittenPosts { get; set; }

        public IList<Meeting> MeetingsAccepted { get; set; }

        public IList<Invite> MeetingsInvitedTo { get; set; }

    }
}