namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseMeetingschanged : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Meetings", name: "Booker_ProfileID", newName: "ProfileId");
            RenameIndex(table: "dbo.Meetings", name: "IX_Booker_ProfileID", newName: "IX_ProfileId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Meetings", name: "IX_ProfileId", newName: "IX_Booker_ProfileID");
            RenameColumn(table: "dbo.Meetings", name: "ProfileId", newName: "Booker_ProfileID");
        }
    }
}
