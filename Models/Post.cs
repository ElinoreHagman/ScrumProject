using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        public string Content { get; set; }
        public DateTime PostDateTime { get; set; }
        public virtual User UserID { get; set; }
        public virtual Category CategoryName { get; set; }

        [InverseProperty("Post")]
        public IList<File> PostFile { get; set; }


    }
}