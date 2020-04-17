namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PublishedWall = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        PostDateTime = c.DateTime(nullable: false),
                        FilePath = c.String(),
                        ProfileID = c.String(maxLength: 128),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.ProfileID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        Forename = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Phonenumber = c.String(),
                        AdminRights = c.Boolean(nullable: false),
                        Meeting_MeetingID = c.Int(),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.Meetings", t => t.Meeting_MeetingID)
                .ForeignKey("dbo.Users", t => t.ProfileID)
                .Index(t => t.ProfileID)
                .Index(t => t.Meeting_MeetingID);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        MeetingID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MeetingDateTime = c.DateTime(nullable: false),
                        ProfileID = c.String(),
                        Booker_ProfileID = c.String(maxLength: 128),
                        Profile_ProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MeetingID)
                .ForeignKey("dbo.Profiles", t => t.Booker_ProfileID)
                .ForeignKey("dbo.Profiles", t => t.Profile_ProfileID)
                .Index(t => t.Booker_ProfileID)
                .Index(t => t.Profile_ProfileID);
            
            CreateTable(
                "dbo.Invites",
                c => new
                    {
                        InviteID = c.Int(nullable: false, identity: true),
                        Accepted = c.Boolean(nullable: false),
                        ProfileID = c.String(maxLength: 128),
                        Meeting = c.Int(nullable: false),
                        MeetingInvite_InviteID = c.Int(),
                    })
                .PrimaryKey(t => t.InviteID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.Invites", t => t.MeetingInvite_InviteID)
                .Index(t => t.ProfileID)
                .Index(t => t.MeetingInvite_InviteID);
            
            CreateTable(
                "dbo.MeetingDateOptions",
                c => new
                    {
                        OptionID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OptionID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingsID = c.String(nullable: false, maxLength: 128),
                        ChosenWall = c.String(),
                        NotificationsOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SettingsID)
                .ForeignKey("dbo.Profiles", t => t.SettingsID)
                .Index(t => t.SettingsID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        Approved = c.Boolean(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.MeetingDateOptionsInvites",
                c => new
                    {
                        MeetingDateOptions_OptionID = c.Int(nullable: false),
                        Invite_InviteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MeetingDateOptions_OptionID, t.Invite_InviteID })
                .ForeignKey("dbo.MeetingDateOptions", t => t.MeetingDateOptions_OptionID, cascadeDelete: true)
                .ForeignKey("dbo.Invites", t => t.Invite_InviteID, cascadeDelete: true)
                .Index(t => t.MeetingDateOptions_OptionID)
                .Index(t => t.Invite_InviteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Posts", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "ProfileID", "dbo.Users");
            DropForeignKey("dbo.Settings", "SettingsID", "dbo.Profiles");
            DropForeignKey("dbo.Messages", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.MeetingDateOptionsInvites", "Invite_InviteID", "dbo.Invites");
            DropForeignKey("dbo.MeetingDateOptionsInvites", "MeetingDateOptions_OptionID", "dbo.MeetingDateOptions");
            DropForeignKey("dbo.Invites", "MeetingInvite_InviteID", "dbo.Invites");
            DropForeignKey("dbo.Invites", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Meetings", "Profile_ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "Meeting_MeetingID", "dbo.Meetings");
            DropForeignKey("dbo.Meetings", "Booker_ProfileID", "dbo.Profiles");
            DropIndex("dbo.MeetingDateOptionsInvites", new[] { "Invite_InviteID" });
            DropIndex("dbo.MeetingDateOptionsInvites", new[] { "MeetingDateOptions_OptionID" });
            DropIndex("dbo.Settings", new[] { "SettingsID" });
            DropIndex("dbo.Messages", new[] { "ProfileID" });
            DropIndex("dbo.Invites", new[] { "MeetingInvite_InviteID" });
            DropIndex("dbo.Invites", new[] { "ProfileID" });
            DropIndex("dbo.Meetings", new[] { "Profile_ProfileID" });
            DropIndex("dbo.Meetings", new[] { "Booker_ProfileID" });
            DropIndex("dbo.Profiles", new[] { "Meeting_MeetingID" });
            DropIndex("dbo.Profiles", new[] { "ProfileID" });
            DropIndex("dbo.Posts", new[] { "Category_CategoryID" });
            DropIndex("dbo.Posts", new[] { "ProfileID" });
            DropTable("dbo.MeetingDateOptionsInvites");
            DropTable("dbo.Users");
            DropTable("dbo.Settings");
            DropTable("dbo.Messages");
            DropTable("dbo.MeetingDateOptions");
            DropTable("dbo.Invites");
            DropTable("dbo.Meetings");
            DropTable("dbo.Profiles");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
