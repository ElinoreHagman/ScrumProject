namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMeeting : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Meetings", "ProfileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meetings", "ProfileID", c => c.String());
        }
    }
}
