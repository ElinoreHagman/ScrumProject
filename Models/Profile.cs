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
        [ForeignKey("User")]
        public string ProfileID { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string Forename { get; set; }
        
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        
        public string Phonenumber { get; set; }
        public bool AdminRights { get; set; }

        public IList<Post> AuthorOfPosts { get; set; }
        public IList<Message> AuthorOfMessages { get; set; }

        public IList<Invite> InviteReciever { get; set; }

        public IList<ProfilesToMeetings> AcceptedMeetings { get; set; }

        public IList<Meeting> CreatorOfMeetings { get; set; }
        public virtual Settings Settings { get; set; }
    }
}