namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTableJunction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MeetingProfiles", "Meeting_MeetingID", "dbo.Meetings");
            DropForeignKey("dbo.MeetingProfiles", "Profile_ProfileID", "dbo.Profiles");
            DropIndex("dbo.MeetingProfiles", new[] { "Meeting_MeetingID" });
            DropIndex("dbo.MeetingProfiles", new[] { "Profile_ProfileID" });
            CreateTable(
                "dbo.ProfilesToMeetings",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        MeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfileID, t.MeetingID })
                .ForeignKey("dbo.Meetings", t => t.MeetingID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileID, cascadeDelete: true)
                .Index(t => t.ProfileID)
                .Index(t => t.MeetingID);
            
            DropTable("dbo.MeetingProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MeetingProfiles",
                c => new
                    {
                        Meeting_MeetingID = c.Int(nullable: false),
                        Profile_ProfileID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Meeting_MeetingID, t.Profile_ProfileID });
            
            DropForeignKey("dbo.ProfilesToMeetings", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ProfilesToMeetings", "MeetingID", "dbo.Meetings");
            DropIndex("dbo.ProfilesToMeetings", new[] { "MeetingID" });
            DropIndex("dbo.ProfilesToMeetings", new[] { "ProfileID" });
            DropTable("dbo.ProfilesToMeetings");
            CreateIndex("dbo.MeetingProfiles", "Profile_ProfileID");
            CreateIndex("dbo.MeetingProfiles", "Meeting_MeetingID");
            AddForeignKey("dbo.MeetingProfiles", "Profile_ProfileID", "dbo.Profiles", "ProfileID", cascadeDelete: true);
            AddForeignKey("dbo.MeetingProfiles", "Meeting_MeetingID", "dbo.Meetings", "MeetingID", cascadeDelete: true);
        }
    }
}
