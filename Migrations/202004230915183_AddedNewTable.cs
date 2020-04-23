namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChosenCategories",
                c => new
                    {
                        ChosenCategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProfileID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ChosenCategoryID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChosenCategories", "ProfileID", "dbo.Profiles");
            DropIndex("dbo.ChosenCategories", new[] { "ProfileID" });
            DropTable("dbo.ChosenCategories");
        }
    }
}
