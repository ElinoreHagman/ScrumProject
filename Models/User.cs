using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public bool Approved { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Profile ProfileID { get; set; }
    }
}