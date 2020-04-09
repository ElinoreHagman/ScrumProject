using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class Invite
    {
        [Key]
        public int InviteID { get; set; }

        public virtual Profile SenderProfileID { get; set; }
        public virtual Profile RecieverProfileID { get; set; }
     
    }
}