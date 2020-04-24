using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class ProfileIndexViewModel
    {
        public List<Profile> Profiles { get; set; }

        public List<Category> Categories { get; set; }

        public List<ChosenCategories> ChosenCategory { get; set; }
    }
}
