using ScrumProject.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class MeetingIndexViewModel
    {
        [Required]
        public string MeetingName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public DateTime Date2 { get; set; }

        [Required]
        public TimeSpan Time2 { get; set; }
    }
}