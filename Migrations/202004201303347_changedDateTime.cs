namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings", "EveryoneAnswered", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invites", "ChosenDate", c => c.DateTime());
            AlterColumn("dbo.Meetings", "MeetingDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meetings", "MeetingDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Invites", "ChosenDate");
            DropColumn("dbo.Meetings", "EveryoneAnswered");
        }
    }
}
