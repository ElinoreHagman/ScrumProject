namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedMeetingName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invites", "MeetingName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invites", "MeetingName");
        }
    }
}
