namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixaMeeting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profiles", "Meeting_MeetingID", "dbo.Meetings");
            DropForeignKey("dbo.Meetings", "Profile_ProfileID", "dbo.Profiles");
            DropIndex("dbo.Profiles", new[] { "Meeting_MeetingID" });
            DropIndex("dbo.Meetings", new[] { "Profile_ProfileID" });
            CreateTable(
                "dbo.MeetingProfiles",
                c => new
                    {
                        Meeting_MeetingID = c.Int(nullable: false),
                        Profile_ProfileID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Meeting_MeetingID, t.Profile_ProfileID })
                .ForeignKey("dbo.Meetings", t => t.Meeting_MeetingID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.Profile_ProfileID, cascadeDelete: true)
                .Index(t => t.Meeting_MeetingID)
                .Index(t => t.Profile_ProfileID);
            
            DropColumn("dbo.Profiles", "Meeting_MeetingID");
            DropColumn("dbo.Meetings", "Profile_ProfileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meetings", "Profile_ProfileID", c => c.String(maxLength: 128));
            AddColumn("dbo.Profiles", "Meeting_MeetingID", c => c.Int());
            DropForeignKey("dbo.MeetingProfiles", "Profile_ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.MeetingProfiles", "Meeting_MeetingID", "dbo.Meetings");
            DropIndex("dbo.MeetingProfiles", new[] { "Profile_ProfileID" });
            DropIndex("dbo.MeetingProfiles", new[] { "Meeting_MeetingID" });
            DropTable("dbo.MeetingProfiles");
            CreateIndex("dbo.Meetings", "Profile_ProfileID");
            CreateIndex("dbo.Profiles", "Meeting_MeetingID");
            AddForeignKey("dbo.Meetings", "Profile_ProfileID", "dbo.Profiles", "ProfileID");
            AddForeignKey("dbo.Profiles", "Meeting_MeetingID", "dbo.Meetings", "MeetingID");
        }
    }
}
