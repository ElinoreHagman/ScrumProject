using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ScrumProject.Models
{
    public class EmailNotifyViewModel
    {
        #region Properties  

        /// <summary>  
        /// Gets or sets to email address.  
        /// </summary>  
        [Required]
        [Display(Name = "To (Email Address)")]
        public string ToEmail { get; set; }
        [Required]
        public string PostMadeIn { get; set; }

        #endregion
    }
}