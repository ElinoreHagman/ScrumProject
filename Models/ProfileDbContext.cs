using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class ProfileDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }

        public ProfileDbContext() : base("ProfileDb") { }
    }
}