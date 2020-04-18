using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScrumProject.Models
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<MeetingDateOptions> MeetingOptions { get; set; }
        public DbSet<MeetingDateOptionsToInvite> MeetingDateOptionsToInvite { get; set; }
        public DbSet<ProfilesToMeetings> ProfilesToMeetings { get; set; }


        public BlogDbContext() : base("BlogDb") { }
    }
}