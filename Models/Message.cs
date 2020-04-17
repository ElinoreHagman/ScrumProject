using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }

        public string ProfileID { get; set; }
        public Profile Author { get; set; }
    }
}