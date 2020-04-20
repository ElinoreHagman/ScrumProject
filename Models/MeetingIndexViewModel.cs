using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class MeetingIndexViewModel
    {
        public string MeetingName { get; set; }

        public DateTime MeetingDate { get; set; }

        public DateTime MeetingTime { get; set; }
    }
}