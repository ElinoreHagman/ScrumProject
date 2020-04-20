using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class MeetingDateOptions
    {
        [Key]
        public int OptionID { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public IList<MeetingDateOptionsToInvite> BelongsToInvites { get; set; }

    }
}