using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ScrumProject.Models;

namespace ScrumProject.Models
{
    public class ChosenCategories
    {
        [Key]
        public int ChosenCategoryID { get; set; }

        public string Name { get; set; }

        [ForeignKey("GetNotificationsOn")]
        public string ProfileID { get; set; }
        public Profile GetNotificationsOn { get; set; }

    }
}