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

        public string PublishedWall { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime PostDateTime { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public IList<File> UploadedFiles { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload")]       
        public string FilePath { get; set; }

        public string ProfileID { get; set; }
        public Profile Author { get; set; }

    }
}