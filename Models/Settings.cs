using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Settings
    {
        [ForeignKey("Profile")]
        public string SettingsID { get; set; }
        public virtual Profile Profile { get; set; }

        public string ChosenWall { get; set; }
        public bool NotificationsOn { get; set; }
    }
}