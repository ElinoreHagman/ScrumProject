using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ScrumProject.Models
{
    public class EmailNotifyViewModel
    {

     
        [Required]
        [Display(Name = "To (Email Address)")]
        public string ToEmail { get; set; }
       

    }
}