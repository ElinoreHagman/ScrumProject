
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class File
    {
        [Key]
        public string Root { get; set; }

        public virtual Post PostID { get; set; }
    }
}