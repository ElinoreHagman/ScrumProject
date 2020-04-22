namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invites", "MeetingID", c => c.Int(nullable: false));
            CreateIndex("dbo.Invites", "MeetingID");
            AddForeignKey("dbo.Invites", "MeetingID", "dbo.Meetings", "MeetingID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invites", "MeetingID", "dbo.Meetings");
            DropIndex("dbo.Invites", new[] { "MeetingID" });
            DropColumn("dbo.Invites", "MeetingID");
        }
    }
}
