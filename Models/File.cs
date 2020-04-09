
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class File
    {
        [Key]
        public string Root { get; set; }

        [ForeignKey("Post")]
        public int PostID { get; set; }
        public Post Post { get; set; }

    }
}