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

        [Required]
        public string PublishedWall { get; set; }

        [Required(ErrorMessage = "You can't leave this field empty")]
        public string Content { get; set; }

        [Required(ErrorMessage = "You can't leave this field empty")]
        public string Title { get; set; }

        [Required]
        public DateTime PostDateTime { get; set; }

        public Category Category { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload")]
        public string FilePath { get; set; }

        [ForeignKey("AuthorOfPosts")]
        public string ProfileID { get; set; }
        public Profile AuthorOfPosts { get; set; }

    }
}