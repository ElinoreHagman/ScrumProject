using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class PostIndexViewModel
    {
        public List<Post> Posts { get; set; }

        public List<Category> Categories { get; set; }

        public List<Profile> Profiles { get; set; }
        public List<Comment> Comments { get; set; }
    }
}