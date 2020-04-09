using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Meeting
    {
        [Key]
        public int MeetingID { get; set; }

        public DateTime MeetingDateTime { get; set; }
  
    }
}