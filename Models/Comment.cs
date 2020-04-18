using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("AuthorOfComments")]
        public string ProfileID { get; set; }
        public Profile AuthorOfComments { get; set; }

        [ForeignKey("Comments")]
        public int PostID { get; set; }
        public Post Comments { get; set; }
    }
}