using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumProject.Models
{
    public class CategoryIndexViewModel
    {
        public List<Category> Categories { get; set; }
        public Post post { get; set; }
    }
}