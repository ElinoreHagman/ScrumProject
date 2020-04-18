using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ScrumProject.Models;

namespace ScrumProject.Models
{
    public class ProfilesToMeetings
    {
        [Key, Column(Order = 0)]
        public string ProfileID { get; set; }
        [Key, Column(Order = 1)]
        public int MeetingID { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}