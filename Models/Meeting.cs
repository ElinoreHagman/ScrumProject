﻿using System;
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

        public string Name { get; set; }

        public DateTime MeetingDateTime { get; set; }

        public IList <Profile> ParticipantsWhoAccepted { get; set; }

        public string ProfileID { get; set; }
        public Profile Booker { get; set; }

    }

}