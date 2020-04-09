using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Profile
    {
        [Key]
        public int ProfileID { get; set; }

        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Phonenumber { get; set; }
        public bool AdminRights { get; set; }
    }
}