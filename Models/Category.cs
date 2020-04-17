using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        
        [Required(ErrorMessage = "The category must have a name")]
        public string Name { get; set; }

        public IList<Post> PostsInCategory { get; set; }
    }
}